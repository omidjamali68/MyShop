using MyShop.Common;
using MyShop.Common.Messages;
using MyShop.Domain.SeedWork;

namespace MyShop.Domain.SharedKernel
{
    public class Name : ValueObject
    {
        public const byte MaxLen = 100;
        public string Value { get; private set; }

        private Name()
        {
        }

        public static Name Create(string name)
        {
            var shopName = new Name();
            if (name.Length > MaxLen)
            {
                shopName.Result.WithError(
                    string.Format(Validations.MaxLenValidation, DataDictionary.ShopName, MaxLen));
                return shopName;
            }
            shopName.Value = name;
            return shopName;
        }
    }
}
