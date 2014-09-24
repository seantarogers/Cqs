namespace Cqs.Application
{
    using Cqs.Infrastructure.EntityFramework;

    public class UnitOfWorkFactory : IUnitOfWorkFactory
    {
        public IUnitOfWork Create()
        {
            return new EfUnitOfWork(new CqsCommandContext());
        }
    }
}