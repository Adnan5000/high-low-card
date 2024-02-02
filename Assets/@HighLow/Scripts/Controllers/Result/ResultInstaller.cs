using Zenject;

namespace HighLow.Scripts.Controllers.Result
{
    public class ResultInstaller : Installer<ResultInstaller>
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesTo<ResultController>().AsSingle();
        }
    }
}