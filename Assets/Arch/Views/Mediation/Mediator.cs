using Arch.Signals;
using UniRx;
using Zenject;

namespace Arch.Views.Mediation
{
    public abstract class Mediator: IMediator
    {
        //protected SignalBus SignalBus { get; private set; }
        protected ISignalService SignalService { get; private set; }
        protected CompositeDisposable DisposeOnDestroy { get; } = new CompositeDisposable();
        
        private DisposableManager _disposableManager;
        private bool _isDisposed;
        private bool _isInitialized;

        protected bool IsDisposed => _isDisposed;
        
        [Inject]
        public void Init(
            //SignalBus signalBus, 
            DisposableManager disposableManager,
            ISignalService signalService
            )
        {
            //SignalBus = signalBus;
            SignalService = signalService;
            _disposableManager = disposableManager;
        }
        
        public virtual void Initialize()
        {
            _isInitialized = true;
            OnMediatorInitialize();
        }


        public void Enable()
        {
            if(!_isInitialized)
                return;
            
            OnMediatorEnable();
        }

        public void Disable()
        {
            if(!_isInitialized)
                return;
            
            OnMediatorDisable();
        }
        
        public void Dispose()
        {
            if(_isDisposed)
                return;
            
            OnMediatorDispose();
            
            _disposableManager.Remove(this);
            
            DisposeOnDestroy.Dispose();
            
            _isDisposed = true;
        }
        
        protected virtual void OnMediatorDispose() {}
        protected virtual void OnMediatorEnable() {}
        protected virtual void OnMediatorDisable() {}
        protected virtual void OnMediatorInitialize() {}
    }
    
    public abstract class Mediator<TView>: Mediator, IMediator<TView>
        where TView : IView
    {
        public TView View { get; private set; }
       

        private IMediatorVisitor<TView>[] _visitors;

        [Inject]
        public void Init(TView view)
        {
            View = view;
        }
        
        [Inject]
        public void Visit(IMediatorVisitor<TView>[] visitors)
        {
            _visitors = visitors;
        }

        public sealed override void Initialize()
        {
            InitializeVisitors();

            base.Initialize();   
            
            if(View.GetGameObject.activeInHierarchy)
                Enable();
        }
        
        private void InitializeVisitors()
        {
            for (var i = 0; i != _visitors.Length; i++)
            {
                _visitors[i].Initialize();
                DisposeOnDestroy.Add(_visitors[i]);
            }
        }
    }
}