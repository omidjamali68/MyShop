using MyShop.Domain.Aggregates.Managers;

namespace MyShop.Domain.Aggregates.Shops
{
    public class ShopManager
    {
        public int ManagerId { get; set; }
        public Manager Manager { get; set; }

        public int ShopId { get; set; }
        public Shop Shop { get; set; }
    }
}
