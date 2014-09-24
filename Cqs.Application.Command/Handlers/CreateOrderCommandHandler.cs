namespace Cqs.Application.Command.Handlers
{
    using System;

    using Cqs.Application.Command.Commands;
    using Cqs.Domain;
    using Cqs.Infrastructure.Dto;
    using Cqs.Infrastructure.EntityFramework;

    public class CreateOrderCommandHandler : IUnitOfWorkCommandHandler<CreateOrderCommand, OrderCreatedDto>
    {
        public OrderCreatedDto Result { get; private set; }

        public CqsCommandContext CqsContext { get; set; }

        public void Handle(CreateOrderCommand command)
        {
            var order = new Order();
            AutoMapper.Mapper.Map(command.OrderDto, order);
            order.CustomerId = command.CustomerId;
            order.PlacedOn = DateTime.Now;
            order.DispatchedOn = DateTime.Now.AddMonths(3);

            CqsContext.Orders.Add(order);
            CqsContext.SaveChanges();

            var domainEvent = new Event
                                  {
                                      DateCreated = DateTime.Now,
                                      Description =
                                          string.Format(
                                              "I just saved an order of {0} for customer {1}",
                                              command.CustomerId,
                                              command.OrderDto.ProductName),
                                      EntityId = order.Id
                                  };
            CqsContext.DomainEvents.Add(domainEvent);
            Result = new OrderCreatedDto { OrderId = order.Id };
        }
    }
}