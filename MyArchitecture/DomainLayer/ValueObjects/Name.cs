namespace MyArchitecture.DomainLayer.ValueObjects
{
    public sealed class Name
    {
        readonly string value;
        const int LENGTH_LIMIT = 255;

        public Name(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException("Un nom ne peut pas être vide.");
            }

            if (value.Length > LENGTH_LIMIT)
            {
                throw new ArgumentOutOfRangeException(nameof(value), value.Length, "Le nom ne peut pas dépasser 255 caractères.");
            }

            this.value = value;
        }

        public override string ToString()
        {
            return value;
        }

        public override bool Equals(object? obj)
        {
            return obj is Name other && this.value == other.value
                   || obj is string otherStr && this.value == otherStr;
        }

        public override int GetHashCode()
        {
            return value.GetHashCode();
        }
    }
}
