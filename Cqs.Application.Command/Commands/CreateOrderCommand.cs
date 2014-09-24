namespace Cqs.Application.Command.Commands
{
    using Cqs.Application.Authorisation;
    using Cqs.Infrastructure.Dto;

    public class CreateOrderCommand : Command, IAuthoriseCreateOrder
    {
        public int CustomerId { get; set; }
        public OrderDto OrderDto { get; set; }
        public string CustomerName { get; set; }

    }
}