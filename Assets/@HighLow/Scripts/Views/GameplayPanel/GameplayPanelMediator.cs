using Arch.InteractiveObjectsSpawnerService;
using Arch.Views.Mediation;
using HighLow.Scripts.Common;
using HighLow.Scripts.Controllers.GameLogic;
using HighLow.Scripts.Controllers.Gameplay;
using Unity.VisualScripting;
using Zenject;

namespace HighLow.Scripts.Views.GameplayPanel
{
    public class GameplayPanelMediator: Mediator<IGameplayPanel>
    {
        private IGameLogicController _gameLogicController;
        private IGameplayController _gameplayController;

        [Inject]
        private void Init(
            IGameLogicController gameLogicController, IGameplayController gameplayController)
        {
            _gameLogicController = gameLogicController;
            _gameplayController = gameplayController;
        }

        protected override void OnMediatorInitialize()
        {
            base.OnMediatorInitialize();
         
            View.HighButtonClicked += OnHigh;
            View.LowButtonClicked += OnLow;
            View.EqualButtonClicked += OnEqual;
        }

        //ToDo: Get ids of card from the hand or gameplaycontroller
        private void OnHigh()
        {
            _gameLogicController.CheckMove(_gameplayController.GetCardId(), EnumsHandler.Moves.High);
        }

        private void OnLow()
        {
            _gameLogicController.CheckMove(_gameplayController.GetCardId(), EnumsHandler.Moves.Low);
        }

        private void OnEqual()
        {
            _gameLogicController.CheckMove(_gameplayController.GetCardId(), EnumsHandler.Moves.Equal);
        }
    }
}