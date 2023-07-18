using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyShop.Domain.Aggregates.Products;

namespace MyShop.Persistence.Products
{
    internal class ProductEntityMap : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey( x => x.Id);

            builder.Property(x => x.Id)
                .ValueGeneratedOnAdd();

            builder.Property(x => x.Quantity)
                .HasConversion(x => x.Value, 
                    x => Domain.Aggregates.Products.ValueObjects.Quantity.Create(x));

            builder.Property(x => x.Name)
                .HasConversion(x => x.Value,
                    x => Domain.SharedKernel.Name.Create(x));
        }
    }
}
