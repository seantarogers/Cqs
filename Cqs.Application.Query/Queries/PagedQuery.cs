namespace Cqs.Application.Query.Queries
{
    using Cqs.Infrastructure.Dto;

    public class PagedQuery<TResult> : Query<TResult>
        where TResult : Dto
    {
        public int Page { get; set; }
        public int ResultsPerPage { get; set; }
    }
}