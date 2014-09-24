namespace Cqs.Application
{
    public interface IUnitOfWorkManager
    {
        void Commit();

        IUnitOfWork GetCurrent();
    }
}