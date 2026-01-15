namespace MyArchitecture.ApplicationLayer;

public abstract class NoResponseUseCase<TRequest>
{
    public abstract void Execute(TRequest request);
}

public abstract class UseCase<TRequest, TResult> : NoResponseUseCase<TRequest>
{
    public event EventHandler<TResult>? ResultSended;
    protected void Send(TResult result)
    {
        ResultSended?.Invoke(this, result);
    }
}

public abstract class UseCase<TRequest> : UseCase<TRequest, Result>;
