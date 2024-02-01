using Zenject;

namespace Arch.AssetReferences
{
    public class AssetReferenceInstaller : Installer<AssetReferenceInstaller>
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesTo<AssetReferenceDownloader>().AsSingle();
        }
    }
}