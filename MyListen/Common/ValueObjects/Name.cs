using MyArchitecture;

namespace MyListen.Common.ValueObjects;

public class Name
{
    readonly string value;
    const int LIMIT = 255;

    private Name(string value)
    {
        this.value = value;
    }

    public static Result<Name> FromString(string value)
    {
        if (string.IsNullOrWhiteSpace(value)) return Result<Name>.Fail("Le nom ne peut pas être vide.");
        if (value.Length > LIMIT) return Result<Name>.Fail($"Le nom est trop long. La longueur maximale est de {LIMIT} caractères.");
        return Result<Name>.Ok(new Name(value));
    }

    public override string ToString()
    {
        return value;
    }

    public static bool operator !=(Name left, Name right) => !(left == right);
    public static bool operator ==(Name left, Name right) => left.Equals(right);

    public static bool operator !=(Name left, string right) => !(left == right);
    public static bool operator ==(Name left, string right) => left.Equals(right);

    public override bool Equals(object? obj)
    {
        return obj is Name other && value == other.value
               || obj is string otherString && value == otherString;
    }
    public override int GetHashCode() => value.GetHashCode();
}
