using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyShop.Domain.Aggregates.Products.Entities;
using MyShop.Domain.SharedKernel;

namespace MyShop.Persistence.Products
{
    internal class CategoryEntityMap : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .ValueGeneratedOnAdd();

            builder.Property(x => x.ParentCategoryId)
                .IsRequired(false);

            builder.Property(x => x.Name)
                .HasMaxLength(Name.MaxLen)
                .HasConversion(x => x.Value, x => Name.Create(x));

            builder.HasIndex(x => new {x.Id, x.Name})
                .IsUnique();
        }
    }
}
