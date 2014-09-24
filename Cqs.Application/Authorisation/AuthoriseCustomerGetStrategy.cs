namespace Cqs.Application.Authorisation
{
    using System.Security;
    using System.Security.Claims;

    public class AuthoriseCustomerGetStrategy : IAuthorisationStrategy
    {
        public bool IsApplicable(IAuthorisable authorisable)
        {
            var authoriseReadCustomer = authorisable as IAuthoriseReadCustomer;
            return authoriseReadCustomer != null;
        }

        public void Authorise(IAuthorisable authorisable)
        {
            if (!ClaimsPrincipal.Current.HasClaim(c => c.Type == ClaimConstants.CanViewCustomerClaim))
            {
                var user = ClaimsPrincipal.Current.Identity.Name;
                throw new SecurityException(
                    string.Format("User {0} does not have a claim to view customer records.", user));
            }
        }
    }
}