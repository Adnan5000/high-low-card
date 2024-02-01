using System;

//using UniRx;

namespace Arch.Signals
{
    public interface ISignalReceiver
    {
        IObservable<TSignal> Receive<TSignal>()
            where TSignal: ISignal;
    }
}