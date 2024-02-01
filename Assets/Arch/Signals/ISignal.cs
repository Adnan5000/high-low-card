namespace Arch.Signals
{
    public interface ISignal
    {
    }
    
    public interface ISignal<TValue>: ISignal
    {
        TValue Value { get; }
    }

    public interface IAsyncSignal: ISignal
    {
    }

    public interface IAsyncSignal<TValue> : ISignal<TValue>, IAsyncSignal
    {
    }
}