namespace Cqs.Application.Authorisation
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class AuthorisationStrategyFactory
    {
        private readonly IEnumerable<IAuthorisable> authorisationStrategies;

        public AuthorisationStrategyFactory(IEnumerable<IAuthorisable> authorisationStrategies)
        {
            this.authorisationStrategies = authorisationStrategies;
        }

        public TInterface GetStrategy<TAuthorisationStrategy, TInterface>() where TInterface : IAuthorisable
            where TAuthorisationStrategy : IAuthorisable
        {
            var strategyType = typeof(TAuthorisationStrategy);
            var strategiesFound = this.authorisationStrategies.Where(s => s.GetType() == strategyType)
                .ToList();

            if (strategiesFound.Count < 1)
            {
                throw new ApplicationException(
                    string.Format(
                        "Could not find Strategy {0} in list of registered Strategies. Check it implements IAuthorisable.",
                        strategyType));
            }
            if (strategiesFound.Count > 1)
            {
                throw new ApplicationException(
                    string.Format("Found multiple matching Strategies for type {0}.", strategyType));
            }

            var state = strategiesFound.First();

            if (state is TInterface)
            {
                return (TInterface)state;
            }

            throw new ApplicationException(
                string.Format("Cannot cast {0} to Interface {1}", strategyType, typeof(TInterface)));
        }
    }
}
