namespace Cqs.Application
{
    using System;

    using Cqs.Infrastructure.EntityFramework;

    public class EfUnitOfWork : IUnitOfWork, IDisposable
    {
        public CqsCommandContext CqsCommandContext { get; private set; }

        public EfUnitOfWork(CqsCommandContext cqsCommandContextcontext)
        {
            this.CqsCommandContext = cqsCommandContextcontext;
        }

        public void Commit()
        {
            this.CqsCommandContext.SaveChanges();
        }

        public void Dispose()
        {
            if (this.CqsCommandContext != null)
            {
                this.CqsCommandContext.Dispose();
                this.CqsCommandContext = null;
            }

            GC.SuppressFinalize(this);
        }
    }
}