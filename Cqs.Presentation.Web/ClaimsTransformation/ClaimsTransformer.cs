
    namespace Cqs.Presentation.Web.ClaimsTransformation
    {
        using System;
        using System.Collections.Generic;
        using System.IdentityModel.Services;
        using System.IdentityModel.Tokens;
        using System.Linq;
        using System.Security.Claims;
        using System.Xml;

        using global::Cqs.Application;

        public class ClaimsTransformer : ClaimsAuthenticationManager
        {
            public override ClaimsPrincipal Authenticate(string resourceName, ClaimsPrincipal incomingPrincipal)
            {
                if (!incomingPrincipal.Identity.IsAuthenticated)
                {
                    return base.Authenticate(resourceName, incomingPrincipal);
                }

                var claimsIdentity = (ClaimsIdentity)incomingPrincipal.Identity;
                return base.Authenticate(resourceName, TransformIncomingClaims(claimsIdentity));
            }

            private static ClaimsPrincipal TransformIncomingClaims(ClaimsIdentity claimsIdentity)
            {
                var nameClaim = claimsIdentity.Claims.First(c => c.Type == ClaimTypes.Name);
                //NB - Here you would query the database - is this user allowed to view customer records?
                //this is only called once per session - and serialized to an auth cookie

                var transformedClaims = CreateNewClaims(nameClaim);
                var transformedClaimsPrincipal = new ClaimsPrincipal(new ClaimsIdentity(transformedClaims));
                SerializeClaimsToCookie(transformedClaimsPrincipal);

                return new ClaimsPrincipal(new ClaimsIdentity(transformedClaims, "Windows"));
            }

            private static List<Claim> CreateNewClaims(Claim nameClaim)
            {
                var nameIdentifierClaim = new Claim(ClaimTypes.NameIdentifier, nameClaim.Value);
                var identityProvderClaim = new Claim(ClaimConstants.IdentityProvider, nameClaim.Value);
                var authMethodClaim = new Claim(ClaimTypes.AuthenticationMethod, new Uri("http://cqs").AbsoluteUri);
                var authInstantClaim = new Claim(
                    ClaimTypes.AuthenticationInstant,
                    XmlConvert.ToString(DateTime.Now, XmlDateTimeSerializationMode.Utc));

                //so for joe bloggs - the database says he can currently view customers and created orders.
                //so we add the claims for the auth handlers.
                var canViewCustomersClaim = new Claim(ClaimConstants.CanViewCustomerClaim, "true");
                var canViewOrdersClaim = new Claim(ClaimConstants.CanCreateOrderClaim, "true");

                return new List<Claim>
                           {
                               identityProvderClaim,
                               nameIdentifierClaim,
                               nameClaim,
                               authInstantClaim,
                               authMethodClaim,
                               canViewCustomersClaim,
                               canViewOrdersClaim
                           };
            }

            private static void SerializeClaimsToCookie(ClaimsPrincipal transformedClaimsPrincipal)
            {
                var sessionSecurityToken = new SessionSecurityToken(
                    transformedClaimsPrincipal,
                    TimeSpan.FromMinutes(30));

                var sessionAuthenticationModule = GetSessionAuthenticationModule();
                sessionAuthenticationModule.WriteSessionTokenToCookie(sessionSecurityToken);
            }

            private static SessionAuthenticationModule GetSessionAuthenticationModule()
            {
                var sessionAuthenticationModule = FederatedAuthentication.SessionAuthenticationModule;
                if (sessionAuthenticationModule == null)
                {
                    throw new ApplicationException("The session authentication module is null");
                }
                return sessionAuthenticationModule;
            }
        }
    }


