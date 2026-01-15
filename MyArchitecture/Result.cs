namespace MyArchitecture;

public sealed class Result
{
    public bool IsSuccess => Failure is null;
    readonly Failure? Failure;

    private Result(Failure? failure = null)
    {
        Failure = failure;
    }

    public static Result Ok()
    {
        return new Result();
    }

    public static Result Fail(string message)
    {
        return new Result(new Failure(message));
    }

    public Failure GetFailure()
    {
        if (Failure is null) throw new InvalidOperationException("Le résultat est positif");
        return Failure;
    }

}

public sealed class Result<T>
{
    public bool IsSuccess => failure is null;
    readonly T? value;
    readonly Failure? failure;

    private Result(T? value = default, Failure? failure = null)
    {
        this.value = value;
        this.failure = failure;
    }

    public static Result<T> Ok(T value)
    {
        return new Result<T>(value);
    }

    public static Result<T> Fail(string message)
    {
        return Fail(new Failure(message));
    }

    public static Result<T> Fail(Failure failure)
    {
        return new Result<T>(failure: failure);
    }

    public T GetValue()
    {
        if (value is null) throw new InvalidOperationException("Le résultat est négatif");
        return value;
    }

    public Failure GetFailure()
    {
        if (failure is null) throw new InvalidOperationException("Le résultat est positif");
        return failure;
    }
}

public record Failure(string BrokenRule);