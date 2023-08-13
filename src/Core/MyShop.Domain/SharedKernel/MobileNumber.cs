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
        public string Value { get; }

        public static explicit operator string(MobileNumber mobile) => mobile.Value; 

        private MobileNumber(string value) 
        {               
            Value = value;
        }

        public static Result<MobileNumber> Create(string value)
        {                      
            if (value.Length != FixLength)
            {
                string errorMessage = string.Format
                    (Validations.FixLengthNumeric,
                    DataDictionary.MobileNumber, FixLength);

                return Result.Failure<MobileNumber>(Error.Create(
                    "Manager.MobileNumber.FixLenError", 
                    string.Format(Validations.FixLengthNumeric,DataDictionary.MobileNumber, FixLength)));
            }

            if (System.Text.RegularExpressions.Regex.IsMatch
                (input: value, pattern: RegularExpression) == false)
            {
                string errorMessage = string.Format
                    (Validations.RegularExpression,
                    DataDictionary.MobileNumber);

                return Result.Failure<MobileNumber>(Error.Create(
                    "Manager.MobileNumber.RegexError",
                    string.Format(Validations.RegularExpression,DataDictionary.MobileNumber)));
            }            

            return new MobileNumber(value); 
        }

        public override IEnumerable<object> GetAtomicValues()
        {
            throw new NotImplementedException();
        }
    }
}
