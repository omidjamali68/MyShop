using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyShop.Domain.Aggregates.Products.Entities;

namespace MyShop.Persistence.Products
{
    internal class ProductFeatureEntityMap : IEntityTypeConfiguration<ProductFeature>
    {
        public void Configure(EntityTypeBuilder<ProductFeature> builder)
        {
            builder.ToTable("ProductFeatures");

            builder.HasKey(x=> x.Id);

            builder.Property(x=> x.Id)
                .ValueGeneratedOnAdd() ;

            builder.HasIndex(x => new {x.ProductId, x.DisplayName})
                .IsUnique() ;
        }
    }
}
