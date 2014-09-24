namespace Cqs.Infrastructure.Dapper
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Threading.Tasks;

    using global::Dapper;

    using DapperExtensions;

    public sealed class DapperConnection : IDapperConnection
    {
        private bool disposed;

        private readonly IDbConnection connection;

        public DapperConnection(string connectionString)
        {
            this.connection = new SqlConnection(connectionString);
        }

        public void Open()
        {
            if (this.connection.State != ConnectionState.Open)
            {
                connection.Open();
            }
        }

        public int Execute(string sql, object param = null, CommandType? commandType = default(CommandType?))
        {
            return connection.Execute(sql, param, null, default(int?), commandType);
        }

        public IEnumerable<dynamic> Query(
            string sql,
            object param = null,
            CommandType? commandType = default(CommandType?))
        {
            return connection.Query(sql, param, null, true, default(int?), commandType);
        }

        public IEnumerable<T> Query<T>(
            string sql,
            object param = null,
            CommandType? commandType = default(CommandType?))
        {
            return connection.Query<T>(sql, param, null, true, default(int?), commandType);
        }

        public IGridReader QueryMultiple(
            string sql,
            object param = null,
            CommandType? commandType = default(CommandType?))
        {
            return new GridReader(connection.QueryMultiple(sql, param, null, default(int), commandType));
        }

        public int Count<T>(object predicate) where T : class
        {
            return connection.Count<T>(predicate);
        }

        public T Get<T>(int id) where T : class
        {
            return connection.Get<T>(id);
        }

        public Task<IEnumerable<T>> QueryAsync<T>(
            string sql,
            object param = null,
            CommandType? commandType = default(CommandType?))
        {
            return connection.QueryAsync<T>(sql, param, null, null, CommandType.StoredProcedure);
        }

        public IEnumerable<T> GetPage<T>(object predicate, IList<ISort> sort, int page, int resultsPerPage)
            where T : class
        {
            return connection.GetPage<T>(predicate, sort, page, resultsPerPage);
        }

        public IEnumerable<T> GetList<T>(object predicate = null, IList<ISort> sort = null) where T : class
        {
            return connection.GetList<T>(predicate, sort);
        }

        private void Dispose(bool disposing)
        {
            if (this.disposed)
            {
                return;
            }

            if (disposing)
            {

                if (this.connection.State != ConnectionState.Closed)
                {
                    this.connection.Dispose();
                }
            }

            disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
