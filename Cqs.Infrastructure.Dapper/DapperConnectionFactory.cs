namespace Cqs.Infrastructure.Dapper
{
    using System;

    public class DapperConnectionFactory : IDapperConnectionFactory
    {
        private readonly ConnectionStringProvider connectionStringProvider;

        public DapperConnectionFactory(ConnectionStringProvider connectionStringProvider)
        {
            this.connectionStringProvider = connectionStringProvider;
        }

        public IDapperConnection CreateConnection()
        {
            return new DapperConnection(connectionStringProvider.ConnectionString);
        }
    }
}

