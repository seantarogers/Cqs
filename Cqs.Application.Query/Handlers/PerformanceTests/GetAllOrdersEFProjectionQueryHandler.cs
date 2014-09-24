namespace Cqs.Application.Query.Handlers.PerformanceTests
{
    using System.Linq;

    using Cqs.Application.Query.Queries.PerformanceTests;
    using Cqs.Infrastructure.Dto;
    using Cqs.Infrastructure.EntityFramework;

    public class GetAllOrdersEFProjectionQueryHandler : IQueryHandler<OrdersDto, GetAllOrdersEfProjectionsQuery>
    {
        public OrdersDto Handle(GetAllOrdersEfProjectionsQuery query)
        {
            using (var cqsQueryContext = new CqsQueryContext())
            {
                cqsQueryContext.Configuration.AutoDetectChangesEnabled = false;
                cqsQueryContext.Configuration.LazyLoadingEnabled = false;
                cqsQueryContext.Configuration.ProxyCreationEnabled = false;

                var orderDtos = cqsQueryContext.Orders.Where(o => o.CustomerId == query.CustomerId)
                    .Select(
                        o =>
                        new OrderDto
                            {
                                Id = o.Id,
                                ProductName = o.ProductName,
                                DispatchedOn = o.DispatchedOn,
                                PlacedOn = o.PlacedOn
                            }).ToList();

                return new OrdersDto { Orders = orderDtos };
            }
        }
    }
}