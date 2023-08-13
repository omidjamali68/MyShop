using MyShop.Common;
using MyShop.Common.Messages;
using MyShop.Domain.SeedWork;

namespace MyShop.Domain.Aggregates.Products.ValueObjects
{
    public class Quantity : ValueObject
    {
        public const int MinValue = 0;
        public int Value { get; }

        private Quantity(int value)
        {
            Value = value;
        }

        public static Result<Quantity> Create(int value)
        {            
            if (value < MinValue)
            {                
                return Result.Failure<Quantity>(Error.Create(
                    "Product.Quantity.MinValueError",
                    string.Format(Validations.MinLenValidation, DataDictionary.Quantity, MinValue))
                    );
            }
            
            return new Quantity(value);
        }

        public override IEnumerable<object> GetAtomicValues()
        {
            throw new NotImplementedException();
        }
    }
}
