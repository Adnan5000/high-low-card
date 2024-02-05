using HighLow.Scripts.Controllers.Stat;
using Zenject;

namespace HighLow.Scripts.Controllers.Time
{
    public class TimeInstaller : Installer<TimeInstaller>
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesTo<TimeController>().AsSingle();
        }
    }
}