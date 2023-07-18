using MyShop.Domain.Aggregates.Managers;
using MyShop.Domain.SeedWork;
using MyShop.Domain.SharedKernel;

namespace MyShop.Domain.Aggregates.Shops
{
    public class Shop : Entity
    {
        public Name Name { get; private set; }
        public string Address { get; private set; }
        public bool IsActive { get; private set; }
        public HashSet<ShopManager> ShopManagers { get; set; }                       

        private Shop()
        {
            ShopManagers = new HashSet<ShopManager>();
        }

        public static Shop Create(string name, string address, bool isActive)
        {               
            var shop = new Shop();
            var shopName = Name.Create(name);

            if (!shopName.Result.IsSucces)
            {
                shop.Result.SetErrors(shopName.Result.Messeges);
                return shop;
            }

            shop.Name = shopName;
            shop.Address = address;
            shop.IsActive = isActive;
            
            return shop;
        }

        public void ChangeStatus()
        {
            if (IsActive)
                IsActive = false;
            else
                IsActive = true;
        }

        public void Update(string name, string address)
        {
            var shopName = Name.Create(name);
            if (!shopName.Result.IsSucces)
            {
                this.Result.SetErrors(shopName.Result.Messeges);
                return;
            }
            Name = shopName;
            Address = address;
        }

        public void AssignNewManager(string firstName, string lastName, byte age, string mobileNumber)
        {
            var manager = Manager.Create(firstName, lastName, age, mobileNumber);
            if (!manager.Result.IsSucces)
            {
                Result.SetErrors(manager.Result.Messeges);
                return;
            }

            ShopManagers.Add(new ShopManager
            {
                Shop = this,
                Manager = manager
            });
        }

        public void AssignExistManager(Manager selectedManager)
        {
            ShopManagers.Add(new ShopManager
            {
                Shop = this,
                Manager = selectedManager
            });
        }
    }
}
