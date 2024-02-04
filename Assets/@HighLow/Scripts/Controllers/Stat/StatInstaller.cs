using HighLow.Scripts.Controllers.Gameplay;
using Zenject;

namespace HighLow.Scripts.Controllers.Stat
{
    public class StatInstaller : Installer<StatInstaller>
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesTo<StatController>().AsSingle();
        }
    }
}