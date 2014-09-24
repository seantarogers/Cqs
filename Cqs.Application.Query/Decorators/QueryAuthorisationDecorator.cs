namespace Cqs.Application.Query.Decorators
{
    using Cqs.Application.Authorisation;
    using Cqs.Application.Query.Handlers;
    using Cqs.Application.Query.Queries;
    using Cqs.Infrastructure.Dto;

    public class QueryAuthorisationDecorator<TDto, TQuery> : IQueryHandler<TDto, TQuery>
        where TQuery : Query<TDto>, IAuthorisable where TDto : Dto
    {
        private readonly IAuthorisationManager authorisationManager;

        private readonly IQueryHandler<TDto, TQuery> decoratedQueryHandler;

        public QueryAuthorisationDecorator(IAuthorisationManager authorisationManager, IQueryHandler<TDto, TQuery> decoratedQueryHandler)
        {
            this.authorisationManager = authorisationManager;
            this.decoratedQueryHandler = decoratedQueryHandler;
        }

        public TDto Handle(TQuery query)
        {
            this.authorisationManager.Authorise(query);

            return this.decoratedQueryHandler.Handle(query);
        }
    }
}