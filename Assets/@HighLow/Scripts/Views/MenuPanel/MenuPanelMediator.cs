using Arch.InteractiveObjectsSpawnerService;
using Arch.Views.Mediation;
using HighLow.Scripts.Views.CardHand;
using Zenject;

namespace HighLow.Scripts.Views.MenuPanel
{
    public class MenuPanelMediator: Mediator<IMenuPanelView>
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
         
            View.PlayButtonClicked += OnPlay;
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