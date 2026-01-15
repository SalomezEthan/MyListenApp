using MyArchitecture;

namespace MyListen.Common.ValueObjects
{
    public sealed class Reference
    {
        readonly string value;
        private Reference(string value)
        {
            this.value = value;
        }

        public static Result<Reference> FromString(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                return Result<Reference>.Fail("Reference cannot be null or empty.");
            }

            return Result<Reference>.Ok(new Reference(value));
        }

        public override string ToString()
        {
            return value;
        }

        public static bool operator !=(Reference left, Reference right) => !(left == right);
        public static bool operator ==(Reference left, Reference right) => left.Equals(right);

        public static bool operator !=(Reference left, string right) => !(left == right);
        public static bool operator ==(Reference left, string right) => left.Equals(right);

        public override bool Equals(object? obj)
        {
            return obj is Reference otherRef && value == otherRef.value 
                   || obj is string otherStr && value == otherStr;
        }

        public override int GetHashCode()
        {
            return value.GetHashCode();
        }
    }
}
