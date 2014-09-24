namespace Cqs.Infrastructure.Dto
{
    using System.Collections.Generic;

    public class OrdersDto : Dto
    {
        public IEnumerable<OrderDto> Orders { get; set; } 
    }
}