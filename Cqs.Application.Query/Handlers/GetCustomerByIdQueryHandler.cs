namespace Cqs.Application.Query.Handlers
{
    using Cqs.Application.Query.Queries;
    using Cqs.Infrastructure.Dapper;
    using Cqs.Infrastructure.Dto;

    public class GetCustomerByIdQueryHandler : IQueryHandler<CustomerDto, GetCustomerByIdQuery>
    {
        private readonly IDapperConnectionFactory dapperConnectionFactory;

        public GetCustomerByIdQueryHandler(IDapperConnectionFactory dapperConnectionFactory)
        {
            this.dapperConnectionFactory = dapperConnectionFactory;
        }

        public CustomerDto Handle(GetCustomerByIdQuery query)
        {
            using (var dapperConnection = dapperConnectionFactory.CreateConnection())
            {
                dapperConnection.Open();

                return dapperConnection.Get<CustomerDto>(query.CustomerId);
            }
        }
    }
}