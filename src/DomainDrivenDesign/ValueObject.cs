using System;
using System.Linq.Expressions;
using System.Reflection;
using static System.Linq.Expressions.Expression;

namespace CWiz.DomainDrivenDesign
{
    public abstract class ValueObject<T> : IEquatable<T>
        where T : ValueObject<T>
    {
        private static readonly Lazy<Func<T, T, bool>> equals = new Lazy<Func<T, T, bool>>(NewEqualsFunc);

        public bool Equals(T other)
        {
            return other != null && equals.Value((T) this, other);
        }

        public sealed override bool Equals(object obj)
        {
            return Equals(obj as T);
        }

        public static bool operator ==(ValueObject<T> a, ValueObject<T> b)
        {
            if (ReferenceEquals(a, null))
                return ReferenceEquals(b, null);

            if (ReferenceEquals(b, null))
                return false;

            return equals.Value((T) a, (T) b);
        }

        public static bool operator !=(ValueObject<T> a, ValueObject<T> b)
        {
            if (ReferenceEquals(a, null))
                return !ReferenceEquals(b, null);

            if (ReferenceEquals(b, null))
                return true;

            return !equals.Value((T) a, (T) b);
        }

        private static Func<T, T, bool> NewEqualsFunc()
        {
            var type = typeof(T);
            var equal = default(Expression);
            var item1 = Parameter(type, "item1");
            var item2 = Parameter(type, "item2");


            using (var properties = type.GetRuntimeProperties().GetEnumerator())
            {
                if (!properties.MoveNext())
                    return (i1, i2) => true;

                // item1.Property == item2.Property
                var property = properties.Current;
                equal = Equal(Property(item1, property), Property(item2, property));

                while (properties.MoveNext())
                {
                    // folder other properties
                    // ( previous ) && ( item1.Property == item2.Property )
                    property = properties.Current;
                    equal = AndAlso(equal, Equal(Property(item1, property), Property(item2, property)));
                }
            }

            var lambda = Lambda<Func<T, T, bool>>(equal, item1, item2);
            return lambda.Compile();
        }
    }
}