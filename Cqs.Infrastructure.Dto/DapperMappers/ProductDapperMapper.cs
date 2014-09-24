namespace Cqs.Infrastructure.Dto.DapperMappers
{
    using Cqs.Infrastructure.Dto;

    using DapperExtensions.Mapper;

    public sealed class ProductDapperMapper : ClassMapper<ProductDto>
    {
        public ProductDapperMapper()
        {
            this.Table("Product");
            this.AutoMap();
        }
    }
}