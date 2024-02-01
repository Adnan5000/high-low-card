using Arch.Views.Mediation;
using HighLow.Scripts.Views.GameplayPanel;
using HighLow.Scripts.Views.MenuPanel;
using Zenject;

namespace HighLow.Scripts.Views.Installers
{
    public class GameplayPanelInstaller : Installer<GameplayPanelInstaller>
    {
        public override void InstallBindings()
        {
            Container.BindViewToMediator<GameplayPanelView, GameplayPanelMediator>();
        }
    }
}