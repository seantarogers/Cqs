namespace Cqs.Application.Query.Handlers.PerformanceTests
{
    using System.Collections.Generic;
    using System.Linq;

    using Cqs.Application.Query.Queries.PerformanceTests;
    using Cqs.Domain;
    using Cqs.Infrastructure.Dto;
    using Cqs.Infrastructure.EntityFramework;

    public class GetAllOrdersEFStandardQueryHandler : IQueryHandler<OrdersDto, GetAllOrdersEfStandardQuery>
    {
        public OrdersDto Handle(GetAllOrdersEfStandardQuery query)
        {
            using (var cqsQueryContext = new CqsQueryContext())
            {
                var orders = cqsQueryContext.Orders.ToList();
                var orderDtos = AutoMapper.Mapper.Map<List<Order>, List<OrderDto>>(orders);
                return new OrdersDto { Orders = orderDtos };
            }
        }
    }
}