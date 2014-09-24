namespace Cqs.Presentation.Web.ViewModels
{
    using Cqs.Infrastructure.Dto;

    public class OrdersViewModel : ViewModel<OrdersDto>
    {
        public string CustomerName { get; set; }
    }
}