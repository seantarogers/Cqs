namespace Cqs.Application.Query.Queries
{
    using Cqs.Application.Authorisation;
    using Cqs.Infrastructure.Dto;

    public class GetAllOrdersByCustomerQuery : Query<OrdersDto>, IAuthoriseReadCustomer
    {
        public int CustomerId { get; set; } 
    }
}