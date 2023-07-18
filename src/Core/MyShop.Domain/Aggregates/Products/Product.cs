using MyShop.Domain.SeedWork;

namespace MyShop.Domain.Aggregates.Products
{
    public class Product : AggregateRoot
    {
        public string Name { get; set; }
        public int Quantity { get; set; }
    }
}
