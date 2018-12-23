using System;

namespace Core
{
    public abstract class Entity : IEquatable<Entity>
    {
        public Entity()
        {
            Id = Guid.NewGuid();
        }

        public Guid Id { get; private set; }

        public override int GetHashCode()
            => Id.GetHashCode();

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(this, obj)) return true;
            return Equals(obj as Entity);
        }

        public bool Equals(Entity other)
        {
            if (other == null) return false;
            return Id == other.Id;
        }
    }
}