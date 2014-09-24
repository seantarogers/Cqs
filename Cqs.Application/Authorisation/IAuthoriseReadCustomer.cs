namespace Cqs.Application.Authorisation
{
    public interface IAuthoriseReadCustomer : IAuthorisable
    {
        int CustomerId { get; set; }
    }
}
