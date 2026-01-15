using MyArchitecture;

namespace MyListen.Common.ValueObjects
{
    public sealed record Volume
    {
        public float Value { get; }

        private Volume(float value)
        {
            Value = value;
        }

        public static Result<Volume> FromFloat(float value)
        {
            if (value < 0 || value > 1) Result<Volume>.Fail("Le volume doit être inclus entre 0 et 1");
            return Result<Volume>.Ok(new Volume(value));
        }
    }
}
