namespace Cqs.Application.Query.Queries
{
    using Cqs.Application.Authorisation;
    using Cqs.Infrastructure.Dto;

    public class GetCustomerByIdQuery : Query<CustomerDto>, IAuthoriseReadCustomer
    {
        public int CustomerId { get; set; }
    }
}