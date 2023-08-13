using MyShop.Common;
using MyShop.Common.Messages;
using MyShop.Domain.SeedWork;
using System.Net.Mail;

namespace MyShop.Domain.SharedKernel
{
    public sealed class Name : ValueObject
    {
        public const byte MaxLen = 100;
        public string Value { get; }

        public static explicit operator string(Name name) => name.Value;

        private Name(string value)
        {
            Value = value;
        }

        public static Result<Name> Create(string name)
        {      
            if (string.IsNullOrWhiteSpace(name))
            {
                return Result.Failure<Name>(Error.Create(
                    "Shop.Name.Empty",
                    string.Format(Validations.StringIsEmpty, DataDictionary.ShopName)));
            }

            if (name.Length > MaxLen)
            {               
                return Result.Failure<Name>(Error.Create(
                    "Shop.Name.MaxLenException", 
                    string.Format(Validations.MaxLenValidation, DataDictionary.ShopName, MaxLen)));
            }
            
            return new Name(name);
        }

        public override IEnumerable<object> GetAtomicValues()
        {
            yield return Value;
        }
    }
}
