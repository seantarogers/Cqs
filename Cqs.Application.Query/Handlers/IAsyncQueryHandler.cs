using System.Threading.Tasks;

using Cqs.Application.Query.Queries;
using Cqs.Infrastructure.Dto;

namespace Cqs.Application.Query.Handlers
{
    public interface IAsyncQueryHandler<in TQuery, TResult>
        where TQuery : Query<TResult> where TResult : Dto
    {
        Task<TResult> HandleAsync(TQuery sprocQuery);
    }
}
