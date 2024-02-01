using System;
using UnityEngine;
using Zenject;

namespace Arch.Views.Mediation
{
    public static class MediationContainerExtensions
    {
        public static void BindViewToMediator<TView, TMediator>(this DiContainer container, int disposableExecutionOrder = 0)
            where TView : Component, IView
            where TMediator : IMediator
        {
            container.Bind<IMediator>().To<TMediator>().FromMethod(context =>
                    InstantiateMediator<TView, TMediator>(context, disposableExecutionOrder))
                .WhenInjectedInto<TView>();

            if (disposableExecutionOrder != 0)
                container.BindDisposableExecutionOrder<TMediator>(disposableExecutionOrder);
        }

        private static TMediator InstantiateMediator<TView, TMediator>(InjectContext context, int disposableExecutionOrder = 0)
            where TView : Component, IView
            where TMediator : IMediator
        {
            
            var view = context.ObjectInstance as TView;

            if (view == null)
                throw new InvalidOperationException(
                    $"Can't instantiate {typeof(TMediator).Name} for {context.ObjectInstance.GetType().Name}");
            
            var args = new[] {(object)view};
            var instance = context.InstantiateWithArgsInternal<TView, TMediator>(view.gameObject, args);
            
            var disposableManager = context.Container.Resolve<DisposableManager>();
            disposableManager.Add(instance, disposableExecutionOrder);
            
            (instance as IInitializable)?.Initialize(); 
            
            return instance;
        }
        
        private static T InstantiateWithArgsInternal<TView, T>(this InjectContext context, GameObject gameObject, object[] args)
            where TView : Component, IView
        {
            var instance =  (typeof(T).IsSubclassOf(typeof(Component)))
                ? context.Container.InstantiateComponent(typeof(T), gameObject, args)
                    .GetComponent<T>()
                : context.Container.Instantiate<T>(args);

            return instance;
        }
    }
}