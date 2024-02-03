using Arch.InteractiveObjectsSpawnerService;
using Arch.Views.Mediation;
using HighLow.Scripts.Common;
using Unity.VisualScripting;
using UnityEngine.SceneManagement;
using Zenject;

namespace HighLow.Scripts.Views.ResultPanels.LostPanel
{
    public class LostPanelMediator: Mediator<ILostPanelView>
    {
        private IInteractiveObjectsManager _interactiveObjectsManager;

        [Inject]
        private void Init(IInteractiveObjectsManager interactiveObjectsManager)
        {
            _interactiveObjectsManager = interactiveObjectsManager;
        }
        
        protected override void OnMediatorInitialize()
        {
            base.OnMediatorInitialize();
         
            View.PlayAgainButtonClicked += OnPlayAgain;
        }

        private void OnPlayAgain()
        {
            View.Remove( () =>
            {
                SceneManager.LoadScene(EnumsHandler.Scenes.Gameplay.ToString());
            });
        }
    }
}