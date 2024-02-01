namespace Arch.Signals
{
    public interface ISignalPublisher
    {
        void Publish<TSignal>(TSignal signal = null)
            where TSignal: class, ISignal, new();
    }
}