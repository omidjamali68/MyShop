using MyShop.Common;
using MyShop.Common.Messages;
using MyShop.Domain.SeedWork;

namespace MyShop.Domain.Aggregates.Products.ValueObjects
{
    public class Quantity : ValueObject
    {
        public const int MinValue = 0;
        public int Value { get; private set; }

        private Quantity()
        {
        }

        public static Quantity Create(int value)
        {
            var quantity = new Quantity();

            if (value < MinValue)
            {
                quantity.Result.WithError(
                    string.Format(Validations.MinLenValidation,DataDictionary.Quantity,MinValue));
                return quantity;
            }

            quantity.Value = value;
            return quantity;
        }
    }
}
