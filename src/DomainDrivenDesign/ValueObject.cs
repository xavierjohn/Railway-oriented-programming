using System;

namespace CWiz.DomainDrivenDesign
{
    public abstract class ValueObject<T> : IEquatable<T>
        where T : ValueObject<T>
    {
        public override bool Equals(object obj)
        {
            if (obj is T valueObject)
                return Equals(valueObject);

            return false;
        }

        //protected abstract bool EqualsCore(T other);

        public override int GetHashCode()
        {
            return GetHashCodeCore();
        }

        protected abstract int GetHashCodeCore();

        public abstract bool Equals(T other);

        public static bool operator ==(ValueObject<T> a, ValueObject<T> b)
        {
            if (ReferenceEquals(a, null))
                return ReferenceEquals(b, null);

            if (ReferenceEquals(b, null))
                return false;

            return a.Equals(b);
        }

        public static bool operator !=(ValueObject<T> a, ValueObject<T> b)
        {
            return !(a == b);
        }
    }
}
