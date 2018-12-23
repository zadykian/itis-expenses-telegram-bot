using System;
using System.Linq;
using System.Reflection;

namespace Core
{
    public abstract class ValueObject : IEquatable<ValueObject>
    {
        private readonly PropertyInfo[] propertyInfos;

        protected ValueObject()
        {
            Id = Guid.NewGuid();
            propertyInfos = GetType()
                .GetProperties(BindingFlags.Public | BindingFlags.Instance)
                .Where(property => property.Name != "Id")
                .ToArray();
        }

        public Guid Id { get; private set; }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(this, obj)) return true;
            return Equals(obj as ValueObject);
        }

        public bool Equals(ValueObject other)
        {
            if (other == null) return false;
            return propertyInfos
                .All(property => AreEqual(property.GetValue(this), property.GetValue(other)));

            bool AreEqual(object left, object right)
            {
                if (left == null) return right == null;
                return left.Equals(right);
            }
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return propertyInfos.Aggregate(0, (hash, prop) =>
                    hash ^ prop.GetValue(this).GetHashCode() *
                    prop.Name.GetHashCode() * 787);
            }
        }
    }
}