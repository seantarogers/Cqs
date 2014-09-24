namespace Cqs.Presentation.Web.SetUp
{
    using Cqs.Domain;
    using Cqs.Infrastructure.Dto;

    public static class AutoMapperSetUp
    {
        public static void ConfigureMaps()
        {
            AutoMapper.Mapper.CreateMap<Order, OrderDto>();
             
            AutoMapper.Mapper.CreateMap<OrderDto, Order>()
                .ForMember(b => b.CustomerId, opt => opt.Ignore())
                .ForMember(b => b.Customer, opt => opt.Ignore());
        }
    }
}