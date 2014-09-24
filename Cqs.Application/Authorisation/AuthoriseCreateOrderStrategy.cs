namespace Cqs.Application.Authorisation
{
    using System.Security;
    using System.Security.Claims;

    public class AuthoriseCreateOrderStrategy : IAuthorisationStrategy
    {
        public bool IsApplicable(IAuthorisable authorisable)
        {
            var authoriseCreateOrder = authorisable as IAuthoriseCreateOrder;
            return authoriseCreateOrder != null;
        }

        public void Authorise(IAuthorisable authorisable)
        {
            if (!ClaimsPrincipal.Current.HasClaim(c => c.Type == ClaimConstants.CanCreateOrderClaim))
            {
                var user = ClaimsPrincipal.Current.Identity.Name;
                throw new SecurityException(string.Format("User {0} does not have a claim to create new orders.", user));
            }
        }
    }
}