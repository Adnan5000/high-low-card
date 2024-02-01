using Arch.AssetReferences;
using Arch.Installers;
using Arch.InteractiveObjectsSpawnerService;
using Arch.Signals;
using Arch.SoundManager;
using Zenject;

namespace DI.Resources
{
    public class CommonInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            MessageBrokerInstaller.Install(Container);
            SignalsInstaller.Install(Container);
            AssetReferenceInstaller.Install(Container);
            InteractiveObjectServiceInstaller.Install(Container);
            SoundManagerInstaller.Install(Container);
        }
    }
}