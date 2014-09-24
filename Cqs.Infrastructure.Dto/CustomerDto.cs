namespace Cqs.Infrastructure.Dto
{
    using System.Collections.Generic;

    public class CustomerDto : Dto
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public bool IsActive { get; set; }

        public string EmailAddress { get; set; }

        public IEnumerable<OrderDto> Orders { get; set; }
    }
}