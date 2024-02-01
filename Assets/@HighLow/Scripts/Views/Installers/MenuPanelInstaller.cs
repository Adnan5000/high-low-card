using Arch.Views.Mediation;
using HighLow.Scripts.Views.MenuPanel;
using Zenject;

namespace HighLow.Scripts.Views.Installers
{
    public class MenuPanelInstaller: Installer<MenuPanelInstaller>
    {
        public override void InstallBindings()
        {
            Container.BindViewToMediator<MenuPanelView, MenuPanelMediator>();
        }
    }
}