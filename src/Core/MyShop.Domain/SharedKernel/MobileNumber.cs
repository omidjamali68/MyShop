using MyShop.Common;
using MyShop.Common.Messages;
using MyShop.Domain.SeedWork;

namespace MyShop.Domain.SharedKernel
{
    public class MobileNumber : ValueObject
    {
        public const int FixLength = 11;

        public const int VerificationKeyFixLength = 6;

        public const string RegularExpression = @"09\d{9}";
        public string Value { get; private set; }

        private MobileNumber() 
        {               
        }

        public static MobileNumber Create(string value)
        {
            var mobile =
                new MobileNumber();                       

            if (value.Length != FixLength)
            {
                string errorMessage = string.Format
                    (Validations.FixLengthNumeric,
                    DataDictionary.MobileNumber, FixLength);

                mobile.Result.WithError(errorMessage);

                return mobile;
            }

            if (System.Text.RegularExpressions.Regex.IsMatch
                (input: value, pattern: RegularExpression) == false)
            {
                string errorMessage = string.Format
                    (Validations.RegularExpression,
                    DataDictionary.MobileNumber);

                mobile.Result.WithError(errorMessage);

                return mobile;
            }

            mobile.Value = value;

            return mobile; 
        }
    }
}
