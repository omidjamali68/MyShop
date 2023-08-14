using MyShop.Domain.Aggregates.Managers;
using MyShop.Domain.SeedWork;

namespace MyShop.Domain.Aggregates.Shops
{
    public class ShopManager
    {
        private ShopManager(Shop shop, Manager manager)
        {
            Shop = shop;
            Manager = manager;
        }

        private ShopManager()
        {            
        }

        public int ManagerId { get; private set; }
        public Manager Manager { get; private set; }

        public int ShopId { get; private set; }
        public Shop Shop { get; private set; }

        public static Result<ShopManager> Create(Shop shop, Manager manager)
        {
            return new ShopManager(shop, manager);
        }
    }
}
