namespace Cqs.Application.Authorisation
{
    using System.Collections.Generic;
    using System.Linq;

    public class AuthorisationManager : IAuthorisationManager
    {
        private readonly IEnumerable<IAuthorisationStrategy> authorisationStrategies;

        public AuthorisationManager(IEnumerable<IAuthorisationStrategy> authorisationStrategies)
        {
            this.authorisationStrategies = authorisationStrategies;
        }

        public void Authorise(IAuthorisable authorisable)
        {
            var applicableStrategies = this.authorisationStrategies.Where(a => a.IsApplicable(authorisable));

            foreach (var applicableStrategy in applicableStrategies)
            {
                applicableStrategy.Authorise(authorisable);
            }
        }
    }
}