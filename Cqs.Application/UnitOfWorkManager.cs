namespace Cqs.Application
{
    using System;
    using System.Web;

    public class UnitOfWorkManager : IUnitOfWorkManager
    {
        private const string Httpcontextkey = "uow";

        private readonly IUnitOfWorkFactory unitOfWorkFactory;

        public UnitOfWorkManager(IUnitOfWorkFactory unitOfWorkFactory)
        {
            this.unitOfWorkFactory = unitOfWorkFactory;
        }

        public void Commit()
        {
            var unitOfWork = GetUnitOfWork();
            if (unitOfWork != null)
            {
                unitOfWork.Commit();
            }

            throw new ApplicationException("Unit of work was null");
        }

        public IUnitOfWork GetCurrent()
        {
            var unitOfWork = GetUnitOfWork();
            if (unitOfWork == null)
            {
                unitOfWork = unitOfWorkFactory.Create();
                SaveUnitOfWork(unitOfWork);
            }

            return unitOfWork;
        }

        private static IUnitOfWork GetUnitOfWork()
        {
            if (HttpContext.Current == null)
            {
                throw new ApplicationException("Http context is null.");
            }

            if (HttpContext.Current.Items.Contains(Httpcontextkey))
            {
                return (IUnitOfWork)HttpContext.Current.Items[Httpcontextkey];
            }

            throw new ApplicationException("Cannot find unit of work in items.");
        }

        private static void SaveUnitOfWork(IUnitOfWork unitOfWork)
        {
            if (HttpContext.Current != null)
            {
                HttpContext.Current.Items[Httpcontextkey] = unitOfWork;
            }

            throw new ApplicationException("Http context is null.");
        }
    }
}