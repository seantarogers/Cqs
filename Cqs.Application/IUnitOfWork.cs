namespace Cqs.Application
{
    using Cqs.Infrastructure.EntityFramework;

    public interface IUnitOfWork
    {
        void Commit();

        CqsCommandContext CqsCommandContext { get; }
    }
}