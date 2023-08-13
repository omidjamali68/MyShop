namespace MyShop.Application.Services.Products.Queries.GetProducts
{
    public record GetProductsDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
        public int Price { get; set; }
        public bool Displayed { get; set; }
        public string Category { get; set; }
        public string Brand { get; set; }
    }
}