namespace Cqs.PerformanceTests
{
    using System.Configuration;

    using Cqs.Application.Authorisation;
    using Cqs.Application.Command.Handlers;
    using Cqs.Application.Query.Handlers;
    using Cqs.Infrastructure.Dapper;
    using Cqs.Presentation.Web.SetUp;

    using SimpleInjector;
    using SimpleInjector.Extensions;

    public static class ContainerManager
    {
        public static Container RegisterComponentsTo(Container container)
        {
            RegisterQueryComponents(container);
            RegisterCommandComponents(container);
            RegisterConfigurationComponents(container);
            RegisterQueryDecorators(container);
            RegisterCommandDecorators(container);

            container.Verify();
            return container;
        }
        
        private static void RegisterQueryComponents(Container container)
        {
            container.Register<IAuthorisationManager, AuthorisationManager>();
            container.RegisterAllImplementationsImplementingInterface<IAuthorisationStrategy>();
            container.Register<IDapperConnectionFactory, DapperConnectionFactory>();
            container.RegisterManyForOpenGeneric(typeof(IAsyncQueryHandler<,>), typeof(GetCustomerByIdQueryHandler).Assembly);
            container.RegisterManyForOpenGeneric(typeof(IQueryHandler<,>), typeof(GetCustomerByIdQueryHandler).Assembly);
        }
        
        private static void RegisterQueryDecorators(Container container)
        {
            container.RegisterDecorator(typeof(IQueryHandler<,>),typeof(Application.Query.Decorators.QueryAuthorisationDecorator<,>));            
        }

        private static void RegisterCommandDecorators(Container container)
        {
            container.RegisterDecorator(typeof(ICommandHandler<,>), typeof(Application.Command.Decorators.CommandAuthorisationDecorator<,>));
            container.RegisterDecorator(typeof(ICommandHandler<,>), typeof(Application.Command.Decorators.UnitOfWorkDecorator<,>));
            
        }

        private static void RegisterCommandComponents(Container container)
        {
            container.RegisterManyForOpenGeneric(typeof(IUnitOfWorkCommandHandler<,>), typeof(CreateOrderCommandHandler).Assembly);
        }

        private static void RegisterConfigurationComponents(Container container)
        {
            container.RegisterSingle<ConnectionStringProvider>();
            container.RegisterInitializer<ConnectionStringProvider>(c =>
            {
                c.ConnectionString = ConfigurationManager.ConnectionStrings["CqsContext"].ConnectionString;
            });
        }
    }
}