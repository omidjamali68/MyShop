using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyShop.Domain.Aggregates.Managers;
using MyShop.Domain.SharedKernel;

namespace MyShop.Persistence.Managers
{
    internal class ManagerEntityMap : IEntityTypeConfiguration<Manager>
    {
        public void Configure(EntityTypeBuilder<Manager> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .ValueGeneratedOnAdd();

            builder.Property(x => x.FirstName)
                .HasMaxLength(100);

            builder.Property(x => x.LastName)
                .HasMaxLength(100);

            builder.Property(x => x.Age)
                .HasConversion(x => x.Value, x => Domain.Aggregates.Managers.ValueObjects.Age.Create(x));

            builder.Property(xx => xx.MobileNumber)
                .HasMaxLength(MobileNumber.FixLength)
                .HasConversion(x => x.Value, x => MobileNumber.Create(x));
        }
    }
}
