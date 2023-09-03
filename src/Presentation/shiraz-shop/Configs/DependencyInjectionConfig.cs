using Autofac;
using MyShop.Persistence;
using MyShop.Application.Interfaces;
using MyShop.Application.Services.AspIdentities.ApplicationUsers;
using MyShop.Domain.Core.RepositoryContracts;
using MyShop.Persistence.Shops;
using MyShop.Domain;
using MyShop.Application.Services.Shops;

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

            container.RegisterAssemblyTypes(typeof(ShopRepository).Assembly)
                 .AssignableTo<IRepository>()
                 .AsImplementedInterfaces()
                 .InstancePerLifetimeScope();

            container.RegisterDecorator<CachedShopRepository, IShopRepository>();
            //container.RegisterGenericDecorator(typeof(RepositoryDecorator<>), typeof(IRepositoryDecorator<>));

            container.RegisterType<UnitOfWork>()
                .As<IUnitOfWork>()
                .InstancePerLifetimeScope();

            container.RegisterType<ApplicationDbContext>()
                .As<IApplicationDbContext>()
                .InstancePerLifetimeScope();
        }
    }
}
