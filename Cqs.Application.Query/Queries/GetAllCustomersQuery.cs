namespace Cqs.Application.Query.Queries
{
    using Cqs.Application.Authorisation;
    using Cqs.Infrastructure.Dto;

    public class GetAllCustomersQuery : PagedQuery<CustomersDto>, IAuthoriseReadCustomer
    {
        public int CustomerId { get; set; }
    }
}