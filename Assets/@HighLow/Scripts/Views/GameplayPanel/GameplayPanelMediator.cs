using Arch.InteractiveObjectsSpawnerService;
using Arch.Views.Mediation;
using HighLow.Scripts.Common;
using HighLow.Scripts.Controllers.GameLogic;
using HighLow.Scripts.Controllers.Gameplay;
using Unity.VisualScripting;
using UnityEngine;
using Zenject;

namespace HighLow.Scripts.Views.GameplayPanel
{
    public class GameplayPanelMediator: Mediator<IGameplayPanel>
    {
        private IGameLogicController _gameLogicController;
        private IGameplayController _gameplayController;
        private IInteractiveObjectsManager _interactiveObjectsManager;

        [Inject]
        private void Init(IGameLogicController gameLogicController, 
            IGameplayController gameplayController, 
            IInteractiveObjectsManager interactiveObjectsManager)
        {
            _gameLogicController = gameLogicController;
            _gameplayController = gameplayController;
            _interactiveObjectsManager = interactiveObjectsManager;
        }

        protected override void OnMediatorInitialize()
        {
            base.OnMediatorInitialize();
         
            View.HighButtonClicked += OnHigh;
            View.LowButtonClicked += OnLow;
            View.EqualButtonClicked += OnEqual;
            
            _gameLogicController.GameLost += OnGameLost;
            _gameLogicController.GameWin += OnGameWin;
        }

        protected override void OnMediatorDispose()
        {
            _gameLogicController.GameLost -= OnGameLost;
            _gameLogicController.GameWin -= OnGameWin;
        }


        private void OnGameWin()
        {
            if(View != null)
                View.Remove(() =>
                {
                    _interactiveObjectsManager.Instantiate("WinPanel", "PopupsContainer");
                });

        }

        private void OnGameLost()
        {
            if(View!= null)
                View.Remove(() =>
                {
                    _interactiveObjectsManager.Instantiate("LostPanel", "PopupsContainer");
                });

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