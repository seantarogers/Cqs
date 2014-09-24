namespace Cqs.Presentation.Web.Controllers
{
    using System.Web.Mvc;

    using Cqs.Application.Query.Handlers;
    using Cqs.Application.Query.Queries;
    using Cqs.Infrastructure.Dto;
    using Cqs.Presentation.Web.ViewModels;
    
    public class CustomerController : Controller
    {
        private readonly IQueryHandler<CustomersDto, GetAllCustomersQuery> getCustomersQueryHandler;

        private readonly IQueryHandler<CustomersDto, GetCustomerByNameQuery> getCustomerByNameQueryHandler;

        public CustomerController(
            IQueryHandler<CustomersDto, GetAllCustomersQuery> getCustomersQueryHandler,
            IQueryHandler<CustomersDto, GetCustomerByNameQuery> getCustomerByNameQueryHandler)
        {
            this.getCustomersQueryHandler = getCustomersQueryHandler;
            this.getCustomerByNameQueryHandler = getCustomerByNameQueryHandler;
        }

        [Route("customers/create"), HttpGet]
        public ViewResult Create()
        {
            return this.View(new CreateCustomerViewModel());
        }

        [Route("customers/create")]
        [HttpPost]
        public ViewResult Create(CreateCustomerViewModel createCustomerViewModel)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(createCustomerViewModel);
            }

            return this.View(new CreateCustomerViewModel());
        }

        [Route("customers")]
        [HttpGet]
        public ViewResult Index()
        {
            var getCustomersQuery = new GetAllCustomersQuery { Page = 1, ResultsPerPage = 10 };
            var getCustomersDto = this.getCustomersQueryHandler.Handle(getCustomersQuery);

            return this.View(new CustomersViewModel { Data = getCustomersDto });
        }

        [Route("customers/search")]
        [HttpPost]
        public ViewResult Search(LastNameSearchViewModel viewModel)
        {
            var query = new GetCustomerByNameQuery { LastName = viewModel.LastName };
            var getCustomersDto = this.getCustomerByNameQueryHandler.Handle(query);

            return this.View(new LastNameSearchViewModel { Customers = getCustomersDto.Customers });
        }

        [Route("customers/search")]
        [HttpGet]
        public ViewResult Search()
        {
            return this.View(new LastNameSearchViewModel());
        }
    }
}