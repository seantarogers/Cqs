namespace Cqs.Presentation.Web.SetUp
{
    using System.Configuration;
    using System.Web;

    using Cqs.Application;
    using Cqs.Application.Authorisation;
    using Cqs.Application.Command.Commands;
    using Cqs.Application.Command.Decorators;
    using Cqs.Application.Command.Handlers;
    using Cqs.Application.Query.Handlers;
    using Cqs.Infrastructure.Dapper;
    using Cqs.Presentation.Web.Controllers;

    using SimpleInjector;
    using SimpleInjector.Extensions;

    public static class ContainerSetup
    {
        public static Container RegisterComponentsTo(Container container)
        {
            RegisterWebComponents(container);
            RegisterQueryComponents(container);
            RegisterCommandComponents(container);
            RegisterConfigurationComponents(container);
            RegisterCommandDecorators(container);
            RegisterQueryDecorators(container);
            RegisterServices(container);
            container.Verify();
            return container;
        }

        private static void RegisterServices(Container container)
        {
            container.Register<IUnitOfWorkFactory, UnitOfWorkFactory>();
            container.Register<IUnitOfWorkManager, UnitOfWorkManager>();
            container
                .RegisterAllImplementationsImplementingInterface<IAuthorisationStrategy>();            
        }

        private static void RegisterWebComponents(Container container)
        {
            container.RegisterMvcControllers(typeof(CustomerController).Assembly);
            container.RegisterMvcAttributeFilterProvider();
            container.Register(typeof(HttpContextBase), () => new HttpContextWrapper(HttpContext.Current));
        }

        private static void RegisterQueryComponents(Container container)
        {
            container.Register<IDapperConnectionFactory, DapperConnectionFactory>();
            container.Register<IAuthorisationManager, AuthorisationManager>();
            container.RegisterManyForOpenGeneric(typeof(IAsyncQueryHandler<,>), typeof(GetCustomerByIdQueryHandler).Assembly);
            container.RegisterManyForOpenGeneric(typeof(IQueryHandler<,>), typeof(GetCustomerByIdQueryHandler).Assembly);
        }

        private static void RegisterCommandDecorators(Container container)
        {
            container.RegisterDecoratorOnlyWhereHandlerImplementsInterface(
                typeof(ICommandHandler<,>),
                typeof(UnitOfWorkDecorator<,>),
                typeof(IUnitOfWorkCommandHandler<,>));

            container.RegisterDecorator(typeof(ICommandHandler<,>), typeof(CommandAuthorisationDecorator<,>));
        }

        private static void RegisterQueryDecorators(Container container)
        {
            container.RegisterDecorator(
                typeof(IQueryHandler<,>),
                typeof(Application.Query.Decorators.QueryAuthorisationDecorator<,>));
        }

        private static void RegisterCommandComponents(Container container)
        {
            container.RegisterManyForOpenGeneric(typeof(IUnitOfWorkCommandHandler<,>), typeof(CreateOrderCommand).Assembly);
            container.RegisterManyForOpenGeneric(typeof(ICommandHandler<,>), typeof(CreateOrderCommand).Assembly);
        }
        
        private static void RegisterConfigurationComponents(Container container)
        {
            container.RegisterSingle<ConnectionStringProvider>();
            container.RegisterInitializer<ConnectionStringProvider>(
                c =>
                    {
                        c.ConnectionString = ConfigurationManager.ConnectionStrings["CqsContext"].ConnectionString;
                    });
        }
    }
}