using MyShop.Domain.Aggregates.Managers;
using MyShop.Domain.SeedWork;
using MyShop.Domain.SharedKernel;

namespace MyShop.Domain.Aggregates.Shops
{
    public sealed class Shop : Entity
    {
        private readonly HashSet<ShopManager> _shopManagers = new();

        public Name Name { get; private set; }        
        public string Address { get; private set; }
        public bool IsActive { get; private set; }
        public IReadOnlyCollection<ShopManager> ShopManagers => _shopManagers;

        private Shop() {}

        private Shop(Name shopName, string address, bool isActive)
        {            
            Name = shopName;
            Address = address;
            IsActive = isActive;
        }

        public static Result<Shop> Create(string name, string address, bool isActive = true)
        {                           
            var shopName = Name.Create(name);

            if (shopName.IsFailure)
            {                
                return Result.Failure<Shop>(shopName.Error);
            }
            
            return new Shop(shopName.Value, address, isActive);
        }

        public Result ChangeStatus()
        {
            if (IsActive)
                IsActive = false;
            else
                IsActive = true;

            return Result.Success();
        }

        public Result Update(string name, string address)
        {
            var shop = Create(name, address, true);

            if (shop.IsFailure)
            {
                return Result.Failure(shop.Error);
            }

            Name = shop.Value.Name;
            Address = shop.Value.Address;

            return Result.Success();
        }

        public Result AssignNewManager(string firstName, string lastName, byte age, string mobileNumber)
        {
            var manager = Manager.Create(firstName, lastName, age, mobileNumber);
            if (manager.IsFailure)
            {
                return Result.Failure<Shop>(manager.Error);
            }

            var shopManagerResult = ShopManager.Create(this, manager.Value);
            if (shopManagerResult.IsFailure)
            {
                return Result.Failure(shopManagerResult.Error);
            }

            _shopManagers.Add(shopManagerResult.Value);

            return Result.Success();
        }

        public Result AssignExistManager(Manager selectedManager)
        {
            var manager = ShopManager.Create(this, selectedManager);

            if (manager.IsFailure)
            {
                return Result.Failure(manager.Error);
            }

            _shopManagers.Add(manager.Value);

            return Result.Success();
        }
    }
}
