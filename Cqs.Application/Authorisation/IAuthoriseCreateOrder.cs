namespace Cqs.Application.Authorisation
{
    using Cqs.Infrastructure.Dto;

    public interface IAuthoriseCreateOrder : IAuthorisable
    {
        int CustomerId { get; set; }        
    }
}