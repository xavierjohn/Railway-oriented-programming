using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq.Expressions;
using System.Reflection;
using static System.Reflection.BindingFlags;
using static System.Linq.Expressions.Expression;

namespace CWiz.DomainDrivenDesign
{
    public abstract class ValueObject<T> : IEquatable<T>
        where T : ValueObject<T>
    {
        private static readonly Lazy<Func<T, T, bool>> equals = new Lazy<Func<T, T, bool>>(NewEqualsFunc);
        private static readonly Lazy<MethodInfo> sequenceEquals = new Lazy<MethodInfo>(NewSequenceEqualsOfT);

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
            Expression equal = Constant(true);
            var item1 = Parameter(type, "item1");
            var item2 = Parameter(type, "item2");

            foreach (var property in type.GetRuntimeProperties())
                AddPropertyToCompareToExpression(property);

            var lambda = Lambda<Func<T, T, bool>>(equal, item1, item2);
            Debug.WriteLine(lambda);
            return lambda.Compile();

            void AddPropertyToCompareToExpression(PropertyInfo property)
            {
                var propertyType = property.PropertyType.GetTypeInfo();
                if (propertyType.IsEnumerable())
                {
                    var method = sequenceEquals.Value.MakeGenericMethod(propertyType.GetItemType());
                    equal = AndAlso(equal, Call(method, Property(item1, property), Property(item2, property)));
                }
                else
                {
                    equal = AndAlso(equal, Equal(Property(item1, property), Property(item2, property)));
                }
            }
        }

        private static MethodInfo NewSequenceEqualsOfT()
        {
            return typeof(ValueObject<T>).GetTypeInfo().GetMethod(nameof(SequenceEqual), NonPublic | Static);
        }

        private static bool SequenceEqual<TItem>(IEnumerable<TItem> a, IEnumerable<TItem> b)
        {
            if (a == null)
                return b == null;

            if (b == null)
                return false;

            using (var i1 = a.GetEnumerator())
            using (var i2 = b.GetEnumerator())
            {
                while (i1.MoveNext())
                    if (!i2.MoveNext() || !i1.Current.Equals(i2.Current))
                        return false;

                if (i2.MoveNext())
                    return false;
            }

            return true;
        }
    }
}