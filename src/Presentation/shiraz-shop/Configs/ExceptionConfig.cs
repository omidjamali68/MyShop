namespace shiraz_shop.Configs
{
    public class ExceptionConfig : MyConfiguration
    {
        public override void ConfigureApplication(IApplicationBuilder app)
        {
            var environment = app.ApplicationServices.GetRequiredService<IWebHostEnvironment>();

            if (environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }           
        }        
    }
}
