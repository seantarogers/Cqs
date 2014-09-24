namespace Cqs.Tools.DatabaseCreator
{
    using System;
    using System.Configuration;
    using System.IO;
    using System.Linq;
    using System.Reflection;

    using Cqs.Infrastructure.Dapper;
    using Cqs.Infrastructure.EntityFramework;

    static class Program
    {
        private static void Main(string[] args)
        {
            Console.WriteLine("Creating Cqs Database...");
            using (var cqsContext = new CqsCommandContext())
            {
                var customer = cqsContext.Customers.FirstOrDefault();
            }

            Console.WriteLine("Created Cqs Database...");
            
            
            var dapperConnectionFactory = new DapperConnectionFactory(new ConnectionStringProvider(){ConnectionString = ConfigurationManager.ConnectionStrings["CqsContext"].ConnectionString});
            using (var dapperConnection = dapperConnectionFactory.CreateConnection())
            {
                dapperConnection.Open();
                Console.WriteLine("Adding Sprocs into the Cqs Database...");

                dapperConnection.Execute(BuildDropSproc("usp_Read_Customer"));
                dapperConnection.Execute(BuildDropSproc("usp_Read_Order"));
                dapperConnection.Execute(BuildSproc("usp_Read_Customer"));
                dapperConnection.Execute(BuildSproc("usp_Read_Order"));

                Console.WriteLine("Inserting data into the Cqs Database...");
             
                dapperConnection.Execute(BuildInsertCustomersSql("Joe", "Bloggs", "JoeBloggs@gmail.com"));
                var customerId = dapperConnection.Query<int>(BuildInsertCustomersSql("Sean", "Rogers", "Sr@gmail.com"))
                    .First();

                AddOrdersToCustomer(dapperConnection, customerId);
            }

            Console.WriteLine("Database successfully created and seeded.");
            
            Console.ReadLine();
        }

        private static void AddOrdersToCustomer(IDapperConnection dapperConnection, int customerId)
        {
            dapperConnection.Execute(BuildInsertOrdersAgainstCustomerSql(customerId));
            dapperConnection.Execute(BuildInsertOrdersAgainstCustomerSql(customerId));
            dapperConnection.Execute(BuildInsertOrdersAgainstCustomerSql(customerId));
            dapperConnection.Execute(BuildInsertOrdersAgainstCustomerSql(customerId));
            dapperConnection.Execute(BuildInsertOrdersAgainstCustomerSql(customerId));
            dapperConnection.Execute(BuildInsertOrdersAgainstCustomerSql(customerId));
            dapperConnection.Execute(BuildInsertOrdersAgainstCustomerSql(customerId));
            dapperConnection.Execute(BuildInsertOrdersAgainstCustomerSql(customerId));
            dapperConnection.Execute(BuildInsertOrdersAgainstCustomerSql(customerId));
            dapperConnection.Execute(BuildInsertOrdersAgainstCustomerSql(customerId));
        }

        private static string BuildSproc(string name)
        {
            var thisAssembly = Assembly.GetExecutingAssembly();
            using (var stream = thisAssembly.GetManifestResourceStream(string.Format("Cqs.Tools.DatabaseCreator.Sql.{0}.sql", name)))
            {
                if (stream == null)
                {
                    throw new ApplicationException("Stream is null.");
                }
                using (var sr = new StreamReader(stream))
                {
                    return sr.ReadToEnd();
                }
            }
        }

        private static string BuildDropSproc(string sprocName)
        {
            return
                string.Format(
                    "IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'{0}') AND type in (N'P', N'PC')) "
                    + "DROP PROCEDURE [dbo].[{0}]",
                    sprocName);
        }

        private static string BuildInsertCustomersSql(string firstName, string lastName, string emailAddress)
        {
            return
                string.Format(
                    "INSERT INTO [dbo].[Customer] ([FirstName] ,[LastName] ,[IsActive] ,[EmailAddress]) VALUES ('{0}' ,'{1}' ,1 ,'{2}') SELECT CAST(SCOPE_IDENTITY() as int)",
                    firstName,
                    lastName,
                    emailAddress);
        }

        private static string BuildInsertOrdersAgainstCustomerSql(int customerId)
        {
            return
                string.Format(
                    "INSERT INTO [dbo].[Order] ([CustomerId] ,[ProductName] ,[PlacedOn] ,[DispatchedOn]) VALUES ({0} ,'policy 03' ,GetDATE() ,GetDATE())",
                    customerId);
        }
    }
}


