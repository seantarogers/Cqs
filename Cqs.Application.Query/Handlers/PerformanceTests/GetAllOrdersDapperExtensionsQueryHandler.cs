namespace Cqs.Application.Query.Handlers.PerformanceTests
{
    using Cqs.Application.Query.Queries.PerformanceTests;
    using Cqs.Infrastructure.Dapper;
    using Cqs.Infrastructure.Dto;

    using DapperExtensions;

    public class GetAllOrdersDapperExtensionsQueryHandler : IQueryHandler<OrdersDto, GetAllOrdersDapperExensionsQuery>
    {
        private readonly IDapperConnectionFactory dapperConnectionFactory;

        public GetAllOrdersDapperExtensionsQueryHandler(IDapperConnectionFactory dapperConnectionFactory)
        {
            this.dapperConnectionFactory = dapperConnectionFactory;
        }

        public OrdersDto Handle(GetAllOrdersDapperExensionsQuery query)
        {
            using (var dapperConnection = this.dapperConnectionFactory.CreateConnection())
            {
                dapperConnection.Open();
                var orderDtos = dapperConnection.GetList<OrderDto>(GetPredicate(query.CustomerId));
                return new OrdersDto { Orders = orderDtos };
            }
        }

        private static IFieldPredicate GetPredicate(int customerId)
        {
            return Predicates.Field<OrderDto>(o => o.CustomerId, Operator.Eq, customerId);
        }
    }
}