using MyShop.Domain.SeedWork;

namespace MyShop.Domain.Aggregates.Products.Entities
{
    public class ProductImage : Entity
    {
        public virtual Product Product { get; private set; }
        public int ProductId { get; private set; }
        public string Src { get; private set; }

        private ProductImage()
        { 
        }

        public static ProductImage Create(int productId, string src)
        {
            var image = new ProductImage();
            image.ProductId = productId;
            image.Src = src;
            return image;
        }
    }
}
