namespace MyShop.Domain.SeedWork
{
    public abstract class ValueObject
    {
        public ValueObject()
        {
            Result = new Result();
        }

        public Result Result { get; }
    }
}
