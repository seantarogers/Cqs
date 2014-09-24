namespace Cqs.Presentation.Web.Controllers
{
    using System.Web.Mvc;

    using Cqs.Application.Command.Commands;
    using Cqs.Application.Command.Handlers;
    using Cqs.Application.Query.Handlers;
    using Cqs.Application.Query.Queries.PerformanceTests;
    using Cqs.Infrastructure.Dto;
    using Cqs.Presentation.Web.ViewModels;

    [RoutePrefix("order")]
    public class OrderController : Controller
    {
        private readonly IQueryHandler<OrdersDto, GetAllOrdersDapperExensionsQuery> getAllOrdersQueryHandler;

        private readonly ICommandHandler<CreateOrderCommand, OrderCreatedDto> createOrderCommandHandler;

        public OrderController(
            IQueryHandler<OrdersDto, GetAllOrdersDapperExensionsQuery> getAllOrdersQueryHandler,
             ICommandHandler<CreateOrderCommand, OrderCreatedDto> createOrderCommandHandler)
        {
            this.getAllOrdersQueryHandler = getAllOrdersQueryHandler;
            this.createOrderCommandHandler = createOrderCommandHandler;
        }

        [Route("customer/{customerId}/{customerName}")]
        [HttpGet]
        public ViewResult Get(int customerId, string customerName)
        {
            var ordersDto = this.getAllOrdersQueryHandler.Handle(new GetAllOrdersDapperExensionsQuery { CustomerId = customerId });
            return this.View(new OrdersViewModel { Data = ordersDto, CustomerName = customerName});
        }

        [Route("create/{customerId}/{customerName}")]
        [HttpGet]
        public ViewResult Add(int customerId, string customerName)
        {
            return this.View(
                "Create",
                new CreateOrderViewModel
                    {
                            CustomerName = customerName,
                            CustomerId = customerId,
                            Data = new OrderDto()
                        });
        }

        [Route("create")]
        [HttpPost]
        public RedirectToRouteResult Create(CreateOrderViewModel createOrderViewModel)
        {
            this.createOrderCommandHandler.Handle(
                new CreateOrderCommand
                    {
                        CustomerId = createOrderViewModel.CustomerId,
                        CustomerName = createOrderViewModel.CustomerName,
                        OrderDto = createOrderViewModel.Data
                    });

            return this.RedirectToAction("get", new { customerId = createOrderViewModel.CustomerId, customerName = createOrderViewModel.CustomerName });
        }
    }
}