using MyShop.Domain.Aggregates.Managers.ValueObjects;
using MyShop.Domain.Aggregates.Shops;
using MyShop.Domain.SeedWork;
using MyShop.Domain.SharedKernel;

namespace MyShop.Domain.Aggregates.Managers
{
    public sealed class Manager : Entity
    {
        private HashSet<ShopManager> _shopManagers = new();

        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public Age Age { get; private set; }
        public MobileNumber MobileNumber { get; private set; }

        public IReadOnlyCollection<ShopManager> ShopeManagers => _shopManagers;

        // for EF-Core
        private Manager()
        {            
        }

        private Manager(string firstName, string lastName, Age age, MobileNumber mobile)
        {            
            FirstName = firstName;
            LastName = lastName;
            Age = age;
            MobileNumber = mobile;
        }        

        public static Result<Manager> Create(string firstName, string lastName, byte age, string mobile)
        {
            var mobileResult = MobileNumber.Create(mobile);
            if (mobileResult.IsFailure)
            {
                return Result.Failure<Manager>(mobileResult.Error);
            }                

            var ageResult = Age.Create(age);
            if (ageResult.IsFailure)
            {
                return Result.Failure<Manager>(ageResult.Error);
            }

            return new Manager(firstName, lastName, ageResult.Value, mobileResult.Value);
        }

        public Result Update(string firstName, string lastName, byte age)
        {
            var ageResult = Age.Create(age);
            if (ageResult.IsFailure)
            {
                return Result.Failure(ageResult.Error);
            }                

            Age = ageResult.Value;
            FirstName = firstName;
            LastName = lastName;

            return Result.Success();
        }
    }
}
