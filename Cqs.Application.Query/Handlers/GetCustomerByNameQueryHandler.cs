namespace Cqs.Application.Query.Handlers
{
    using System.Data;

    using Cqs.Application.Query.Queries;
    using Cqs.Infrastructure.Dapper;
    using Cqs.Infrastructure.Dto;

    public class GetCustomerByNameQueryHandler : IQueryHandler<CustomersDto, GetCustomerByNameQuery>
    {
        private readonly IDapperConnectionFactory dapperConnectionFactory;

        public GetCustomerByNameQueryHandler(IDapperConnectionFactory dapperConnectionFactory)
        {
            this.dapperConnectionFactory = dapperConnectionFactory;
        }

        public CustomersDto Handle(GetCustomerByNameQuery query)
        {
            using (var dapperConnection = this.dapperConnectionFactory.CreateConnection())
            {
                const string SprocName = "usp_Read_Customer";
                dapperConnection.Open();
                
                var customers = dapperConnection.Query<CustomerDto>(
                    SprocName,
                    new { query.LastName },
                    CommandType.StoredProcedure);

                return new CustomersDto { Customers = customers };
            }
        }
    }
}