using Zenject;

namespace HighLow.Scripts.Controllers.Gameplay
{
    public class GameplayInstaller : Installer<GameplayInstaller>
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesTo<GameplayController>().AsSingle();
        }
    }
}