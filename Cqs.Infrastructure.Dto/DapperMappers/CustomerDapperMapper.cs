namespace Cqs.Infrastructure.Dto.DapperMappers
{
    using Cqs.Infrastructure.Dto;

    using DapperExtensions.Mapper;

    public sealed class CustomerDapperMapper : ClassMapper<CustomerDto>
    {
        public CustomerDapperMapper()
        {
            this.Table("Customer");
            Map(m => m.Orders).Ignore();
            this.AutoMap();
        }
    }
}