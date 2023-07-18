using Autofac;
using MyShop.Persistence;
using MyShop.Application.Interfaces;
using MyShop.Application.Services.AspIdentities.ApplicationUsers;
using MyShop.Domain.Core.RepositoryContracts;

namespace shiraz_shop.Configs
{
    public class DependencyInjectionConfig : MyConfiguration
    {        
        public override void Initialized()
        {
            //_dbConnectionString = AppSettings.GetConnectionString("DbConnectionString");
        }

        public override void ConfigureServiceContainer(ContainerBuilder container)
        {
            container.RegisterAssemblyTypes(typeof(ApplicationUserService).Assembly)
                 .AssignableTo<IService>()
                 .AsImplementedInterfaces()
                 .InstancePerLifetimeScope();

            container.RegisterType<ApplicationDbContext>()
                .As<IApplicationDbContext>()
                .InstancePerLifetimeScope();

            //container.RegisterAssemblyTypes(typeof(UserRepository).Assembly)
            //     .AssignableTo<IRepository>()
            //     .AsImplementedInterfaces()
            //     .InstancePerLifetimeScope();
        }
    }
}
