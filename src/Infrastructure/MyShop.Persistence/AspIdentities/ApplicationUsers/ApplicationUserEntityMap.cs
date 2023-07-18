using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyShop.Domain.AspIdentities;

namespace MyShop.Persistence.AspIdentities.ApplicationUsers
{
    public class ApplicationUserEntityMap : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            builder.ToTable("ApplicationUsers");

            builder.HasKey(_ => _.Id);

            builder.Property(_ => _.Id)
                .ValueGeneratedOnAdd();

            builder.Property(_ => _.FirstName)
                .IsRequired(false)
                .HasMaxLength(100);

            builder.Property(_ => _.LastName)
                .IsRequired(false)
                .HasMaxLength(100);

            builder.Property(_ => _.NationalCode)
                .IsRequired(false)
                .HasMaxLength(10);

            builder.Property(_ => _.CreationDate);

        }
    }
}
