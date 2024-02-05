using Arch.InteractiveObjectsSpawnerService;
using Arch.Views.Mediation;
using HighLow.Scripts.Controllers.Stat;
using HighLow.Scripts.Views.CardHand;
using UnityEngine;
using Zenject;

namespace HighLow.Scripts.Views.MenuPanel
{
    public class MenuPanelMediator: Mediator<IMenuPanelView>
    {
        private IInteractiveObjectsManager _interactiveObjectsManager;
        private IStatController _statController;

        [Inject]
        private void Init(IInteractiveObjectsManager interactiveObjectsManager,
            IStatController statController)
        {
            _interactiveObjectsManager = interactiveObjectsManager;
            _statController = statController;
        }

        protected override void OnMediatorInitialize()
        {
            base.OnMediatorInitialize();
         
            View.PlayButtonClicked += OnPlay;
            View.ResetButtonClicked += OnReset;
        }

        private void OnReset()
        {
            _statController.DeleteData();
        }

        private void OnPlay()
        {
            View.Remove(() =>
            {
                _interactiveObjectsManager.Instantiate("GameplayPanel", "GamplayUIContainer");
                _interactiveObjectsManager.Instantiate("CardHand", "CardHandContainer");
            });
            
            
        }
    }
}