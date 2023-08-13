using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyShop.Domain.Aggregates.Shops;
using MyShop.Domain.SharedKernel;

namespace MyShop.Persistence.Shops
{
    internal class ShopEntityMap : IEntityTypeConfiguration<Shop>
    {
        public void Configure(EntityTypeBuilder<Shop> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .ValueGeneratedOnAdd();

            builder.Property(x => x.Name)
                .HasMaxLength(Name.MaxLen)
                .HasConversion(x => x.Value, x => Name.Create(x).Value);

            

        }
    }
}
