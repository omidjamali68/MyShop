using Microsoft.AspNetCore.Identity;
using MyShop.Domain.AspIdentities;
using MyShop.Persistence;
using shiraz_shop.Infra;

namespace shiraz_shop.Configs
{
    public class SeedDataConfig : MyConfiguration
    {        

        public async override void ConfigureApplication(IApplicationBuilder app)
        {
            using (var scope = app.ApplicationServices.CreateScope())
            {                                
                var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
                var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<ApplicationRole>>();
                var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

                if (!context.Roles.Any())
                {
                    var roles = new List<string>
                    {
                        UserRoles.Admin,
                        UserRoles.Member,
                        UserRoles.Manager
                    };

                    foreach (var role in roles)
                    {
                        var applicationRole = new ApplicationRole { Name = role };
                        await roleManager.CreateAsync(applicationRole);
                    }
                }

                if (!context.Set<ApplicationUser>().Any())
                {
                    var admin = GenerateAdmin();
                    var createdResult = await userManager.CreateAsync(admin, "P@ssw0rd");
                    if (createdResult.Succeeded)
                        await userManager.AddToRoleAsync(admin, UserRoles.Admin);
                }
            }
        }

        private ApplicationUser GenerateAdmin()
        {
            return new ApplicationUser
            {
                FirstName = "Admin",
                LastName = "Admin",
                CreationDate = DateTime.Now,
                PhoneNumber = "9372216032",
                NationalCode = "2280113732",
                UserName = "9372216032",
                NormalizedUserName = "9372216032",
                PhoneNumberConfirmed = true,
                EmailConfirmed = true
            };
        }
    }
}