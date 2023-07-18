using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MyShop.Domain.AspIdentities;
using MyShop.Persistence;
using shiraz_shop.Configs;
using shiraz_shop.Infra;

namespace shiraz_shop
{
    public class Startup
    {        
        private static MyConfiguration[] _myConfigs;
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            _myConfigs = Program.MyConfigurations;
            Configuration = configuration;
        }

        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            //if (System.Diagnostics.Debugger.IsAttached == false)
            //{
            //    System.Diagnostics.Debugger.Launch();
            //}

            var container = new ContainerBuilder();

            _myConfigs.ForEach(c =>
            {
                c.ConfigureServiceContainer(services);
                c.ConfigureServiceContainer(container);
            });

            container.Populate(services);
            return new AutofacServiceProvider(container.Build());

        }

        //It's For Migration
        //public void ConfigureServices(IServiceCollection services)
        //{
        //    var conn = Configuration.GetConnectionString("DbConnectionString");

        //    services.AddDbContext<ApplicationDbContext>(option =>
        //        option.UseSqlServer(conn)
        //    );

        //    services.AddIdentity<ApplicationUser, ApplicationRole>(option =>
        //    {
        //        option.SignIn.RequireConfirmedAccount = true;
        //        option.Password.RequiredLength = 8;
        //        option.Password.RequireDigit = false;
        //        option.User.AllowedUserNameCharacters = "1234567890";
        //        option.Lockout.AllowedForNewUsers = false;
        //    })
        //    .AddEntityFrameworkStores<ApplicationDbContext>()
        //    .AddDefaultTokenProviders();

        //}

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            _myConfigs.ForEach(c => c.ConfigureApplication(app));                        
        }
    }
}
