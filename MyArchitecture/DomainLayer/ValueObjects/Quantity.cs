
namespace MyArchitecture.DomainLayer.ValueObjects
{
    public sealed record Quantity : IComparable<Quantity>
    {
        public int Count { get; }

        public Quantity(int quantity)
        {
            if (quantity < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(quantity), quantity, "Impossible d'avoir une quantité négative");
            }

            this.Count = quantity;
        }

        public static Quantity operator +(Quantity a, Quantity b)
        {
            return new Quantity(a.Count + b.Count);
        }

        public static Quantity operator -(Quantity a, Quantity b)
        {
            if (a < b) throw new ArgumentOutOfRangeException(nameof(b), b.Count, $"{nameof(b)} ne peut pas être supérieur à A (actuel {a.Count})");
            return new Quantity(a.Count - b.Count);
        }

        public static bool operator >(Quantity a, Quantity b) => a.CompareTo(b) > 0;
        public static bool operator <(Quantity a, Quantity b) => a.CompareTo(b) < 0;
        public static bool operator >=(Quantity a, Quantity b) => a == b || a > b;
        public static bool operator <=(Quantity a, Quantity b) => a == b || a < b;

        public int CompareTo(Quantity? other)
        {
            if (other is null) return 1;
            return this.Count.CompareTo(other.Count);
        }
    }
}
