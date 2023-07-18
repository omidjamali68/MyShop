using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MyShop.Domain.AspIdentities;
using MyShop.Persistence;

namespace shiraz_shop.Configs
{
    public class AspIdentityDbContextConfig : MyConfiguration
    {
        public override void ConfigureServiceContainer(IServiceCollection services)
        {
            var conn = AppSettings.GetConnectionString("DbConnectionString");

            services.AddDbContext<ApplicationDbContext>(option =>
                option.UseSqlServer(conn)
            );

            services.AddIdentity<ApplicationUser, ApplicationRole>(option =>
                {
                    option.SignIn.RequireConfirmedAccount = true;
                    option.Password.RequiredLength = 8;
                    option.Password.RequireDigit = false;
                    option.User.AllowedUserNameCharacters = "1234567890";
                    option.Lockout.AllowedForNewUsers = false;
                })
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();            
        }
    }
}