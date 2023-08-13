namespace MyShop.Domain.SeedWork
{    
    public class Result
    {
        protected internal Result(bool isSuccess, Error error)
        {
            Messeges = new HashSet<string>();

            if (isSuccess && error != Error.None)            
                throw new InvalidOperationException(); 
            
            if (!isSuccess && error == Error.None)
                throw new InvalidOperationException(); 

            IsSuccess = isSuccess;
            Error = error;
        }

        public bool IsSuccess { get; }
        public Error Error { get; }
        public bool IsFailure => !IsSuccess;
        public HashSet<string> Messeges { get; private set; }

        public static Result Success() => new(true, Error.None);
        
        public void WithError(string errorMessage)
        {
            Messeges.Add(errorMessage);
            //IsSuccess = false;
        }

        public void SetErrors(HashSet<string> messeges)
        {
            Messeges = messeges;
            //IsSuccess = false;
        }

        public static Result<TType> Success<TType>(TType type) => new(type, true, Error.None);

        public static Result<T> Failure<T>(Error error) => new(default, false, error);

        public static Result Failure(Error error) => new(false, error);

    }

    public class Result<TValue> : Result
    {
        private readonly TValue _value;
        protected internal Result(TValue value, bool isSuccess, Error error)
            : base(isSuccess, error) =>
            _value = value;

        public TValue Value => IsSuccess && _value is not null
            ? _value!
            : throw new InvalidOperationException("The value of a failure result can not be accessed");

        public static implicit operator Result<TValue>(TValue value) => 
            new(value, true, Error.None);
        
    }
}
