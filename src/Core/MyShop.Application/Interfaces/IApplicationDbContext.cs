using Microsoft.EntityFrameworkCore;
using MyShop.Domain.Aggregates.Managers;
using MyShop.Domain.Aggregates.Products;
using MyShop.Domain.Aggregates.Shops;
using MyShop.Domain.AspIdentities;

namespace MyShop.Application.Interfaces
{
    public interface IApplicationDbContext
    {
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<ApplicationRole> ApplicationRoles { get; set; }
        public DbSet<ApplicationUserRole> ApplicationUserRoles { get; set; }
        public DbSet<Shop> Shops { get; set; }
        public DbSet<Manager> Managers { get; set; }
        public DbSet<Product> Products { get; set; }


        int SaveChanges(bool acceptAllChangesOnSuccess);
        int SaveChanges();
        Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = new CancellationToken());
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken());
    }
}
