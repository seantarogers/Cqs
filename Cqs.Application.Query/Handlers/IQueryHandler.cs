namespace Cqs.Application.Query.Handlers
{
    using Cqs.Application.Authorisation;
    using Cqs.Infrastructure.Dto;

    public interface IQueryHandler<out TDto, in TQuery>
        where TDto : Dto where TQuery : Queries.Query<TDto>, IAuthorisable
    {
        TDto Handle(TQuery query);
    }
}
