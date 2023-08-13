using MyShop.Domain.Aggregates.Products.Entities;
using MyShop.Domain.Aggregates.Products.ValueObjects;
using MyShop.Domain.SeedWork;
using MyShop.Domain.SharedKernel;

namespace MyShop.Domain.Aggregates.Products
{
    public sealed class Product : AggregateRoot
    {
        private HashSet<ProductImage> _productImages = new();
        private HashSet<ProductFeature> _productFeatures = new();

        public Name Name { get; private set; }
        public Quantity Quantity { get; private set; }
        public string Brand { get; private set; }
        public string Description { get; private set; }
        public int Price { get; private set; }
        public bool Displayed { get; private set; }
        public int CategoryId { get; private set; }
        public  Category Category { get; private set; }
        public IReadOnlyCollection<ProductImage> ProductImages => _productImages;
        public IReadOnlyCollection<ProductFeature> ProductFeatures => _productFeatures;        

        private Product() { }

        public Product(
            Name name, 
            Quantity quantity, 
            string brand, 
            string description, 
            int price, 
            bool displayed, 
            int categoryId)
        {
            Name = name;
            Quantity = quantity;
            Brand = brand;
            Description = description;
            Price = price;
            Displayed = displayed;
            CategoryId = categoryId;
        }

        public static Result<Product> Create(
            string name, int quantity, string brand, string description, int price, bool displayed, int categoryId)
        {            
            var quantityResult = Quantity.Create(quantity);
            if (quantityResult.IsFailure)
            {
                return Result.Failure<Product>(quantityResult.Error);
            }

            var nameResult = Name.Create(name);
            if (nameResult.IsFailure)
            {
                return Result.Failure<Product>(nameResult.Error);
            }            

            return new Product(
                nameResult.Value, quantityResult.Value, brand, description, price, displayed, categoryId);
        }

        public void Delete()
        {            
        }
    }
}
