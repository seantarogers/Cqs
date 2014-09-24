using System;

namespace Cqs.PerformanceTests
{
    using System.Configuration;
    using System.Linq;

    using Cqs.Application.Query.Handlers.PerformanceTests;
    using Cqs.Application.Query.Queries.PerformanceTests;
    using Cqs.Infrastructure.Dapper;
    using Cqs.Presentation.Web.SetUp;

    using SimpleInjector;

    static class Program
    {
        private const int Loops = 50000;

        private static void Main(string[] args)
        {
            var container = new Container();
            ContainerManager.RegisterComponentsTo(container);

            int customerId = 0;
            var dapperConnectionFactory =
                new DapperConnectionFactory(
                    new ConnectionStringProvider()
                        {
                            ConnectionString =
                                ConfigurationManager.ConnectionStrings["CqsContext"]
                                .ConnectionString
                        });
            using (var dapperConnection = dapperConnectionFactory.CreateConnection())
            {
                dapperConnection.Open();
                customerId =
                    dapperConnection.Query<int>("Select Id from Customer where  LastName like 'Rogers'")
                        .First();
            }

            Console.WriteLine("NB - this test takes about 2 mins to run. Stick with it, it is worth it!");
            Console.WriteLine("");

            RunEfStandardQueries(container, customerId);
            RunEfProjectionQueries(container, customerId);
            RunDapperSprocQueries(container, customerId);
            RunDapperExtensionsQueries(container, customerId);
            RunDapperAsyncQueries(container, customerId);

            Console.ReadLine();
        }

        private static async void RunDapperAsyncQueries(Container container, int customerId)
        {
            var dapperhandler = container.GetInstance<GetAllOrdersDapperAsyncQueryHandler>();
            var startTime = DateTime.Now;
            Console.WriteLine("==Dapper Async straight to Dtos==");
            var i = 0;
            while (i <= Loops)
            {
                await dapperhandler.HandleAsync(new GetAllOrdersDapperAsyncQuery { CustomerId = customerId });
                i++;
            }
            WriteOutFinishTime(startTime);         
        }

        private static void RunDapperExtensionsQueries(Container container, int customerId)
        {
            var dapperhandler = container.GetInstance<GetAllOrdersDapperExtensionsQueryHandler>();
            var startTime = DateTime.Now;
            Console.WriteLine("==Dapper Extensions (dynamic sql) straight to Dtos==");
            var i = 0;
            while (i <= Loops)
            {
                dapperhandler.Handle(new GetAllOrdersDapperExensionsQuery { CustomerId = customerId});
                i++;
            }
            WriteOutFinishTime(startTime);
        }

        private static void RunEfProjectionQueries(Container container, int customerId)
        {
            var efProjectionQueryHandler = container.GetInstance<GetAllOrdersEFProjectionQueryHandler>();
            var startTime = DateTime.Now;
            Console.WriteLine("==EF Projections and overheads turned off straight to Dtos ==");

            var i = 0;
            while (i <= Loops)
            {
                efProjectionQueryHandler.Handle(new GetAllOrdersEfProjectionsQuery { CustomerId = customerId });
                i++;
            }
            WriteOutFinishTime(startTime);
        }

        private static void RunEfStandardQueries(Container container, int customerId)
        {
            var efProjectionhandler = container.GetInstance<GetAllOrdersEFStandardQueryHandler>();
            AutoMapperSetUp.ConfigureMaps();
            var startTime = DateTime.Now;
            Console.WriteLine("==EF Standard Domain models mapped with automapper to Dtos==");

            var i = 0;
            while (i <= Loops)
            {
                efProjectionhandler.Handle(new GetAllOrdersEfStandardQuery { CustomerId = customerId });
                i++;
            }
            WriteOutFinishTime(startTime);
        }

        private static void RunDapperSprocQueries(Container container, int customerId)
        {
            var dapperSprocHandler = container.GetInstance<GetAllOrdersDapperSprocQueryHandler>();
            var startTime = DateTime.Now;
            Console.WriteLine("==Dapper straight to Dtos ==");

            var i = 0;
            while (i <= Loops)
            {
                dapperSprocHandler.Handle(new GetAllOrdersDapperSprocQuery { CustomerId = customerId });
                i++;
            }
            WriteOutFinishTime(startTime);            
        }

        private static void WriteOutFinishTime(DateTime startTime)
        {
            var now = DateTime.Now;

            TimeSpan duration = now - startTime;
            Console.WriteLine("time taken to execute 50000 selects of 10 records = {0}", duration);
            Console.WriteLine("====");
        }
    }
}
