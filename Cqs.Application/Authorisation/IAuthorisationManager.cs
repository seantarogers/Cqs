namespace Cqs.Application.Authorisation
{
    public interface IAuthorisationManager
    {
        void Authorise(IAuthorisable authorisable);
    }
}