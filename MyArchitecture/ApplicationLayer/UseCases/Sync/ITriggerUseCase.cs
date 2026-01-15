namespace MyArchitecture.ApplicationLayer.UseCases.Sync;

public abstract class NoResponseTriggerUseCase
{
    public abstract void Execute();
}

public abstract class TriggerUseCase<TResult> : NoResponseTriggerUseCase
{
    public event EventHandler<TResult>? ResultSended;
    protected void Send(TResult result)
    {
        ResultSended?.Invoke(this, result);
    }
}


