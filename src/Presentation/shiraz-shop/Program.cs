using System.Diagnostics;
using System.Reflection;
using shiraz_shop.Configs;
using shiraz_shop.Infra;

namespace shiraz_shop
{
    public class Program
    {
        public static MyConfiguration[] MyConfigurations;
        public static void Main(string[] args)
        {
            var baseDirectory = Directory.GetCurrentDirectory();
            var appSettings = ReadAppSettings(args, baseDirectory);
            var environment = appSettings.GetValue("environment", "Development");
            
            MyConfigurations =
                GetConfiguratorsFromAssembly(typeof(Program).Assembly, args, appSettings, baseDirectory);

            //CreateHostBuilder(args).Build().Run();

            InitializeConfigurators();

            var host = new WebHostBuilder()
                .UseContentRoot(baseDirectory)
                .UseEnvironment(environment)                
                .ConfigureLogging(ConfigLogging)
                .UseStartup<Startup>();
            
            ConfigServer(host);

            host.Build().Run();
        }

        private static void InitializeConfigurators()
        {
            MyConfigurations.ForEach(_ => _.Initialized());
        }

        private static void ConfigLogging(WebHostBuilderContext context, ILoggingBuilder logging)
        {
            MyConfigurations.ForEach(configurator => configurator.ConfigureLogging(context, logging));
        }

        private static void ConfigServer(IWebHostBuilder hostBuilder)
        {
            MyConfigurations.ForEach(_ => _.ConfigureServer(hostBuilder));
        }

        private static IConfiguration ReadAppSettings(string[] args, string baseDirectory)
        {
            return new ConfigurationBuilder()
                .SetBasePath(baseDirectory)
                .AddJsonFile(Path.Combine(baseDirectory, "..", "config", "appsettings.json"), true, true)
                .AddJsonFile(Path.Combine(baseDirectory, "appsettings.json"), true, true)
                .AddEnvironmentVariables()
                .AddCommandLine(args)
                .Build();
        }

        private static MyConfiguration[] GetConfiguratorsFromAssembly(
            Assembly assembly,
            string[] args,
            IConfiguration appSettings,
            string baseDirectory)
        {
            void SetPropertyValue<T>(object obj, string name, object value)
            {
                typeof(T).GetProperty(name)?.SetValue(obj, value);
            }

            return assembly.GetTypes()
                .Where(_ => _.IsAbstract == false)
                .Where(typeof(MyConfiguration).IsAssignableFrom)
                .Select(configuratorType => new
                {
                    Type = configuratorType,
                    Config = configuratorType.GetCustomAttribute<ConfigurationAttribute>()
                })
                .Where(_ => _.Config?.Disabled != true)
                .OrderBy(_ => _.Config?.Order ?? 0)
                .Select(_ =>
                {
                    var configurator = Activator.CreateInstance(_.Type) as MyConfiguration;
                    SetPropertyValue<MyConfiguration>(configurator, nameof(MyConfiguration.CommandLineArgs), args);
                    SetPropertyValue<MyConfiguration>(configurator, nameof(MyConfiguration.BaseDirectory), baseDirectory);
                    SetPropertyValue<MyConfiguration>(configurator, nameof(MyConfiguration.AppSettings), appSettings);
                    return configurator;
                })
                .ToArray();
        }

        // Do not remove this because add-migration get error
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
