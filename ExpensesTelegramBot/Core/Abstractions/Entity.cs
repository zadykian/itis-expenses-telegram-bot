using System;

namespace Core
{
    public abstract class Entity : IEquatable<Entity>
    {
        public int Id { get; private set; }

        public override int GetHashCode()
            => Id.GetHashCode();

        public override bool Equals(object obj)
            => Equals(obj as Entity);

        public bool Equals(Entity other)
        {
            if (other == null) return false;
            return Id == other.Id;
        }
    }
}