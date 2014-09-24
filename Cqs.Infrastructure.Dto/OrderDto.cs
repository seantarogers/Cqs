namespace Cqs.Infrastructure.Dto
{
    using System;

    public class OrderDto : Dto
    {
        public int Id { get; set; }

        public string ProductName { get; set; }

        public DateTime PlacedOn { get; set; }

        public DateTime? DispatchedOn { get; set; }

        public int CustomerId { get; set; }
    }
}