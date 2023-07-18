using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyShop.Domain.AspIdentities;

namespace MyShop.Persistence.EntityConfigurations
{
    public class ApplicationUserRoleEntityMap : IEntityTypeConfiguration<ApplicationUserRole>
    {        
        public void Configure(EntityTypeBuilder<ApplicationUserRole> builder)
        {
            builder.ToTable("ApplicationUserRoles");

            builder.HasOne(ur => ur.User)
                   .WithMany(u => u.Roles)
                   .HasForeignKey(ur => ur.UserId)
                   .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(ur => ur.Role)
                   .WithMany(_ => _.Users)
                   .HasForeignKey(ur => ur.RoleId);                   
        }        

    }
}
