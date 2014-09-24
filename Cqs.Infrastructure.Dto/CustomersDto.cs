namespace Cqs.Infrastructure.Dto
{
    using System.Collections.Generic;

    public class CustomersDto : Dto
    {
        public IEnumerable<CustomerDto> Customers { get; set; }
    }
}