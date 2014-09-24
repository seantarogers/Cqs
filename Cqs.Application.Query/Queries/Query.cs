namespace Cqs.Application.Query.Queries
{
    using Cqs.Infrastructure.Dto;

    public abstract class Query<TResult> : Dto where TResult : Dto
    {
    }
}