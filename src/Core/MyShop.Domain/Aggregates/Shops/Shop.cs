using MyShop.Domain.Aggregates.Managers;
using MyShop.Domain.SeedWork;

namespace MyShop.Domain.Aggregates.Shops
{
    public class Shop : Entity
    {
        public string Name { get; private set; }
        public string Address { get; private set; }
        public bool IsActive { get; private set; }
        public Result Result { get;}
        public HashSet<ShopManager> ShopManagers { get; set; }

        private Shop()
        {
            ShopManagers = new HashSet<ShopManager>();
            IsActive = true;
            Result = new Result();
        }             

        public static Shop Create(string name, string address, bool isActive)
        {     
            var result = new Shop();
            result.Name = name;
            result.Address = address;
            result.IsActive = isActive;
            
            return result;
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
            Name = name;
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
