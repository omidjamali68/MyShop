using MyShop.Domain.SeedWork;

namespace MyShop.Domain.Aggregates.Products.Entities
{
    public class ProductFeature : Entity
    {
        public virtual Product Product { get; private set; }
        public int ProductId { get; private set; }
        public string DisplayName { get; private set; }
        public string Value { get; private set; }

        private ProductFeature()
        {
        }

        public static ProductFeature Create(int productId, string displayName, string value)
        {
            var feature = new ProductFeature();

            feature.ProductId = productId;
            feature.DisplayName = displayName;
            feature.Value = value;

            return feature;
        }
    }
}
