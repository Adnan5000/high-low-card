using Arch.Views.Mediation;
using HighLow.Scripts.Common;
using UnityEngine.SceneManagement;

namespace HighLow.Scripts.Views.ResultPanels.WinPanel
{
    public class WinPanelMediator: Mediator<IWinPanelView>
    {
        protected override void OnMediatorInitialize()
        {
            base.OnMediatorInitialize();
         
            View.PlayAgainButtonClicked += OnPlayAgain;
        }

        private void OnPlayAgain()
        {
            View.Remove( () =>
            {
                SceneManager.LoadScene(EnumsHandler.Scenes.MainMenu.ToString());
            });
        }
    }
}