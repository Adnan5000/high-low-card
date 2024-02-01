namespace Arch.Signals
{
    public class Signal: ISignal
    {
    }

    public class Signal<TValue> : ISignal<TValue>
    {
        public TValue Value { get; set; }

        public Signal()
        {
        }
        
        public Signal(TValue value) => Value = value;
    }
    
    public class AsyncSignal: IAsyncSignal
    {
    }

    public class AsyncSignal<TValue> : IAsyncSignal<TValue>
    {
        public TValue Value { get; set; }

        public AsyncSignal()
        {
        }
        
        public AsyncSignal(TValue value) => Value = value;
    }
}