namespace Cqs.Application.Query.Handlers.PerformanceTests
{
    using System.Data;
    using System.Threading.Tasks;

    using Cqs.Application.Query.Queries.PerformanceTests;
    using Cqs.Infrastructure.Dapper;
    using Cqs.Infrastructure.Dto;

    public class GetAllOrdersDapperAsyncQueryHandler : IAsyncQueryHandler<GetAllOrdersDapperAsyncQuery, OrdersDto>
    {
        private readonly IDapperConnectionFactory dapperConnectionFactory;

        public GetAllOrdersDapperAsyncQueryHandler(IDapperConnectionFactory dapperConnectionFactory)
        {
            this.dapperConnectionFactory = dapperConnectionFactory;
        }

        public async Task<OrdersDto> HandleAsync(GetAllOrdersDapperAsyncQuery sprocQuery)
        {
            using (var dapperConnection = this.dapperConnectionFactory.CreateConnection())
            {
                dapperConnection.Open();
                const string SprocName = "usp_Read_Order";

                var orders = await dapperConnection.QueryAsync<OrderDto>(
                        SprocName,
                        new { sprocQuery.CustomerId },
                        CommandType.StoredProcedure);

                return new OrdersDto { Orders = orders };

            }
        }
    }
}