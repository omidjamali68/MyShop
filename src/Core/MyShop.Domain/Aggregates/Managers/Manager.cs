using MyShop.Domain.Aggregates.Managers.ValueObjects;
using MyShop.Domain.Aggregates.Shops;
using MyShop.Domain.SeedWork;
using MyShop.Domain.SharedKernel;

namespace MyShop.Domain.Aggregates.Managers
{
    public class Manager : Entity
    {
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public Age Age { get; private set; }
        public MobileNumber MobileNumber { get; private set; }
        public Result Result { get;}

        public HashSet<ShopManager> ShopeManagers { get; set; }

        private Manager()
        {
            ShopeManagers = new HashSet<ShopManager>();
            Result = new Result();
        }

        private Manager(string firstName, string lastName, Age age, MobileNumber mobile)
        {
            FirstName = firstName;
            LastName = lastName;
            MobileNumber = mobile;
            Age = age;
        }

        public static Manager Create(string firstName, string lastName, byte age, string mobile)
        {
            var result = new Manager();

            var mobileResult = MobileNumber.Create(mobile);
            if (!mobileResult.Result.IsSucces)
            {
                result.Result.SetErrors(mobileResult.Result.Messeges);
                return result;
            }                

            var ageResult = Age.Create(age);
            if (!ageResult.Result.IsSucces)
            {
                result.Result.SetErrors(ageResult.Result.Messeges);
                return result;
            }

            result.MobileNumber = mobileResult;
            result.Age = ageResult;
            result.FirstName = firstName;
            result.LastName = lastName;

            //return new Manager(firstName, lastName, ageResult, mobileResult);
            return result;
        }

        public void Update(string firstName, string lastName, byte age)
        {
            var ageResult = Age.Create(age);
            if (!ageResult.Result.IsSucces)
            {
                Result.SetErrors(ageResult.Result.Messeges); 
                return;
            }                

            Age = ageResult;
            FirstName = firstName;
            LastName = lastName;
        }
    }
}
