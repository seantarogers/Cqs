namespace Cqs.Presentation.Web.ViewModels
{
    using Cqs.Infrastructure.Dto;

    public class CreateOrderViewModel : ViewModel<OrderDto>
    {
        public string CustomerName { get; set; }
        public int CustomerId { get; set; }

    }
}