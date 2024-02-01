using Zenject;

namespace Arch.SoundManager
{
    public class SoundManagerInstaller : Installer<SoundManagerInstaller>
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesTo<SoundManager>().AsSingle();
        }
    }
}