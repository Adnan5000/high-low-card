using System;
using UniRx;
using Zenject;

namespace Arch.Utils
{
    public class LateDisposableWrapper: ILateDisposable
    {
        private readonly CompositeDisposable _compositeDisposable = new CompositeDisposable();

        public LateDisposableWrapper Add(IDisposable disposable)
        {
            _compositeDisposable.Add(disposable);
            return this;
        }
        
        public void LateDispose()
        {
            //_compositeDisposable.Dispose();
        }
    }
}