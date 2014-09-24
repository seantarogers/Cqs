namespace Cqs.Application.Query.Handlers.PerformanceTests
{
    using System.Data;

    using Cqs.Application.Query.Queries.PerformanceTests;
    using Cqs.Infrastructure.Dapper;
    using Cqs.Infrastructure.Dto;

    public class GetAllOrdersDapperSprocQueryHandler : IQueryHandler<OrdersDto, GetAllOrdersDapperSprocQuery>
    {
        private readonly IDapperConnectionFactory dapperConnectionFactory;

        public GetAllOrdersDapperSprocQueryHandler(IDapperConnectionFactory dapperConnectionFactory)
        {
            this.dapperConnectionFactory = dapperConnectionFactory;
        }

        public OrdersDto Handle(GetAllOrdersDapperSprocQuery query)
        {
            using (var dapperConnection = this.dapperConnectionFactory.CreateConnection())
            {
                dapperConnection.Open();
                const string SprocName = "usp_Read_Order";
               var orders = dapperConnection.Query<OrderDto>(
                  SprocName,
                    new { query.CustomerId },
                    CommandType.StoredProcedure);

               return new OrdersDto { Orders = orders };
                
            }
        }
    }
}