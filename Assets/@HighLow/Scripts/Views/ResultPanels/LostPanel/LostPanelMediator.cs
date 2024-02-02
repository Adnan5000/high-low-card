using Arch.Views.Mediation;
using HighLow.Scripts.Common;
using Unity.VisualScripting;
using UnityEngine.SceneManagement;

namespace HighLow.Scripts.Views.ResultPanels.LostPanel
{
    public class LostPanelMediator: Mediator<ILostPanelView>
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