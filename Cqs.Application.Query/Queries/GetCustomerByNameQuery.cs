namespace Cqs.Application.Query.Queries
{
    using Cqs.Application.Authorisation;
    using Cqs.Infrastructure.Dto;

    public class GetCustomerByNameQuery : Query<CustomersDto>, IAuthorisable
    {
        public string LastName { get; set; }
    }
}