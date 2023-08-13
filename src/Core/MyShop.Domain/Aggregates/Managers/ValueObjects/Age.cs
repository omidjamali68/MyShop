using MyShop.Common;
using MyShop.Common.Messages;
using MyShop.Domain.SeedWork;

namespace MyShop.Domain.Aggregates.Managers.ValueObjects
{
    public class Age : ValueObject
    {
        public const byte MinAge = 18;
        public byte Value { get; }        

        private Age(byte age) 
        { 
            Value = age;
        }

        public static Result<Age> Create(byte age)
        {            
            if (age < MinAge)
            {                
                return Result.Failure<Age>(Error.Create(
                    "Manager.Age.MinAgeError", 
                    string.Format(Validations.MinLenValidation, DataDictionary.Age, MinAge)));
            }
            
            return new Age(age);
        }

        public override IEnumerable<object> GetAtomicValues()
        {
            throw new NotImplementedException();
        }
    }
}
