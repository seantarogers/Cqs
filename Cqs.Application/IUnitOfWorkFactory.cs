namespace Cqs.Application
{
    public interface IUnitOfWorkFactory
    {
        IUnitOfWork Create();
    }
}