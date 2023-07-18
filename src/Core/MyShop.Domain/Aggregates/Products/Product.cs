using MyShop.Domain.Aggregates.Products.Entities;
using MyShop.Domain.Aggregates.Products.ValueObjects;
using MyShop.Domain.SeedWork;
using MyShop.Domain.SharedKernel;

namespace MyShop.Domain.Aggregates.Products
{
    public class Product : AggregateRoot
    {
        public Name Name { get; private set; }
        public Quantity Quantity { get; private set; }
        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }
        public virtual ICollection<ProductImage> ProductImages { get; private set; }

        private Product()
        {
        }

        public static Product Create(string name, int quantity)
        {
            var product = new Product();

            var quantityResult = Quantity.Create(quantity);
            if (!quantityResult.Result.IsSucces)
            {
                product.Result.SetErrors(quantityResult.Result.Messeges);
                return product;
            }
            product.Quantity = quantityResult;

            var nameResult = Name.Create(name);
            if (!nameResult.Result.IsSucces)
            {
                product.Result.SetErrors(nameResult.Result.Messeges);
                return product;
            }
            product.Name = nameResult;

            return product;
        }
    }
}
