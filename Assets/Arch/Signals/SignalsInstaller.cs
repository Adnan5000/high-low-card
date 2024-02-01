using Zenject;

namespace Arch.Signals
{
    public class SignalsInstaller: Installer<SignalsInstaller>
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesTo<SignalService>().AsSingle();
        }
    }
}