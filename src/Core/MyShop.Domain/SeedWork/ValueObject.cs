namespace MyShop.Domain.SeedWork
{
    public abstract class ValueObject : IEquatable<ValueObject>
    {
        public bool Equals(ValueObject? other)
        {
            return other is not null && ValuesAreEqual(other);
        }

        public abstract IEnumerable<object> GetAtomicValues();

        private bool ValuesAreEqual(ValueObject other)
        {
            return GetAtomicValues().SequenceEqual(other.GetAtomicValues());
        }

        public override int GetHashCode()
        {
            return GetAtomicValues()
                .Aggregate(default(int), HashCode.Combine);
        }
    }
}
