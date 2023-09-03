namespace shiraz_shop.Configs
{
    public class MvcConfig : MyConfiguration
    {
        public override void ConfigureServiceContainer(IServiceCollection container)
        {
            container.AddMediatR(cfg => 
                cfg.RegisterServicesFromAssembly(MyShop.Application.AssemblyReference.Assembly));

            container.AddStackExchangeRedisCache(redisOption => 
            {
                string connection = AppSettings.GetConnectionString("Redis");
                redisOption.Configuration = connection;
            });
            
            container.AddControllersWithViews();
            container.AddRouting();            

            container.ConfigureApplicationCookie(op => op.LoginPath = "/Home/Login");
        }

        public override void ConfigureApplication(IApplicationBuilder app)
        {
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();            

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                  name: "areas",
                  pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
                );
            });

            app.UseEndpoints(endpoin => 
                endpoin.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}")
                );           
        }
    }
}
