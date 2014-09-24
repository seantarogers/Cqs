using System;
using System.Collections.Generic;
using System.Data;
using DapperExtensions;

namespace Cqs.Infrastructure.Dapper
{
    using System.Threading.Tasks;

    public interface IDapperConnection : IDisposable
    {
        void Open();

        int Execute(string sql, object param = null, CommandType? commandType = default(CommandType?));

        IEnumerable<object> Query(string sql, object param = null, CommandType? commandType = default(CommandType?));

        IEnumerable<T> Query<T>(string sql, object param = null, CommandType? commandType = default(CommandType?));

        IGridReader QueryMultiple(string sql, object param = null, CommandType? commandType = default(CommandType?));

        int Count<T>(object predicate) where T : class;

        T Get<T>(int id) where T : class;

        IEnumerable<T> GetPage<T>(object predicate, IList<ISort> sort, int page, int resultsPerPage) where T : class;

        IEnumerable<T> GetList<T>(object predicate = null, IList<ISort> sort = null) where T : class;

        Task<IEnumerable<T>> QueryAsync<T>(
            string sql,
            object param = null,
            CommandType? commandType = default(CommandType?));
    }
}
