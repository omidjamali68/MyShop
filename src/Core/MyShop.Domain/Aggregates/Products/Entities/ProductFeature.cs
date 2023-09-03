using MyShop.Domain.SeedWork;

namespace MyShop.Domain.Aggregates.Products.Entities
{
    public sealed class ProductFeature : Entity
    {
        public Product Product { get; private set; }
        public int ProductId { get; private set; }
        public string DisplayName { get; private set; }
        public string Value { get; private set; }

        private ProductFeature()
        {
        }

        public static Result<ProductFeature> Create(Product product, string displayName, string value)
        {
            var feature = new ProductFeature();

            feature.Product = product;
            feature.DisplayName = displayName;
            feature.Value = value;

            return feature;
        }
    }
}
