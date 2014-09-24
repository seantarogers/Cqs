namespace Cqs.Application.Query.Queries.PerformanceTests
{
    using Cqs.Application.Authorisation;
    using Cqs.Infrastructure.Dto;

    public class GetAllOrdersDapperExensionsQuery : Query<OrdersDto>, IAuthorisable
    {
        public int CustomerId { get; set; } 
    }
}