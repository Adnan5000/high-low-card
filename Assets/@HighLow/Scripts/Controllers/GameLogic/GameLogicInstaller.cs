using HighLow.Scripts.Controllers.CardPriority;
using Zenject;

namespace HighLow.Scripts.Controllers.GameLogic
{
    public class GameLogicInstaller : Installer<GameLogicInstaller>
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesTo<GameLogicController>().AsSingle();
        }
    }
}