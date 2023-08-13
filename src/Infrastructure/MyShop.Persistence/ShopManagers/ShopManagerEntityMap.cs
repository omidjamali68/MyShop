using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyShop.Domain.Aggregates.Shops;

namespace MyShop.Persistence.ShopManagers
{
    internal class ShopManagerEntityMap : IEntityTypeConfiguration<ShopManager>
    {
        public void Configure(EntityTypeBuilder<ShopManager> builder)
        {
            builder.ToTable("ShopManagers");

            builder.HasKey(x => new {x.ShopId, x.ManagerId});

            builder.HasOne(x => x.Shop)
                .WithMany(x => x.ShopManagers)
                .HasForeignKey(x => x.ShopId);
            
            builder.HasOne(x => x.Manager)
                .WithMany(x => x.ShopeManagers)
                .HasForeignKey(x => x.ManagerId)
                .OnDelete(DeleteBehavior.Restrict); 
        }
    }
}
