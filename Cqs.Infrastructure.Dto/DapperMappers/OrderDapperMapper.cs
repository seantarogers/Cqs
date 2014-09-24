namespace Cqs.Infrastructure.Dto.DapperMappers
{
    using Cqs.Infrastructure.Dto;

    using DapperExtensions.Mapper;

    public sealed class OrderDapperMapper : ClassMapper<OrderDto>
    {
        public OrderDapperMapper()
        {
            this.Table("Order");
            this.AutoMap();
        }
    }
}