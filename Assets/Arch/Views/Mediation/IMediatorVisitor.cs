using System;
using Zenject;

namespace Arch.Views.Mediation
{
    public interface IMediatorVisitor : IDisposable, IInitializable
    {
    }
    
    public interface IMediatorVisitor<TView>: IMediatorVisitor
        where TView: IView
    {
        [Inject]
        void Init(IMediator<TView> mediator);
    }
}