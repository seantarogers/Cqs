namespace Cqs.Application.Query.Queries.PerformanceTests
{
    using Cqs.Application.Authorisation;
    using Cqs.Infrastructure.Dto;

    public class GetAllOrdersDapperAsyncQuery : Query<OrdersDto>, IAuthorisable
    {
        public int CustomerId { get; set; } 
    }
}