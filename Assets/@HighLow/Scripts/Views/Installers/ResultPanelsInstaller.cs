using Arch.Views.Mediation;
using HighLow.Scripts.Views.MenuPanel;
using HighLow.Scripts.Views.ResultPanels.LostPanel;
using HighLow.Scripts.Views.ResultPanels.WinPanel;
using Zenject;

namespace HighLow.Scripts.Views.Installers
{
    public class ResultPanelsInstaller: Installer<ResultPanelsInstaller>
    {
        public override void InstallBindings()
        {
            Container.BindViewToMediator<WinPanelView, WinPanelMediator>();
            Container.BindViewToMediator<LostPanelView, LostPanelMediator>();
        }
    }
}