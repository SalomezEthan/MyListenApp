namespace MyArchitecture.DomainLayer
{
    public abstract class Entity (Guid id)
    {
        public Guid Id { get; init; } = id;

        public static bool operator !=(Entity a, Entity b)
        {
            return !(a == b);
        }

        public static bool operator ==(Entity a, Entity b)
        {
            return a.Equals(b);
        }

        public override bool Equals(object? obj)
        {
            return obj is Entity other && Id == other.Id;
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }
}
