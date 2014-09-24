namespace Cqs.Infrastructure.Dto
{
    public class ProductDto : Dto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public bool InStock { get; set; }

        public int Price { get; set; }

        public string Location { get; set; }
    }
}