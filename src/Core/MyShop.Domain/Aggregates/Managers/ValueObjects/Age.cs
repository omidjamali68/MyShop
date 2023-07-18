using MyShop.Common;
using MyShop.Common.Messages;
using MyShop.Domain.SeedWork;

namespace MyShop.Domain.Aggregates.Managers.ValueObjects
{
    public class Age : ValueObject
    {
        public const byte MinAge = 18;
        public byte Value { get; private set; }

        private Age() 
        { 
        }

        public static Age Create(byte age)
        {
            var result = new Age();

            if (age < MinAge)
            {
                result.Result.WithError(
                    string.Format(Validations.MinLenValidation,DataDictionary.Age, MinAge));
            }

            result.Value = age;
            return result;
        }

    }
}
