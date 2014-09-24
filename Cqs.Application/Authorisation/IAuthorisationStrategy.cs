namespace Cqs.Application.Authorisation
{
    public interface IAuthorisationStrategy
    {
        bool IsApplicable(IAuthorisable authorisable);

        void Authorise(IAuthorisable authorisable);
    }
}