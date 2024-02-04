using Arch.InteractiveObjectsSpawnerService;
using Arch.Views.Mediation;
using HighLow.Scripts.Common;
using HighLow.Scripts.Controllers.GameLogic;
using HighLow.Scripts.Controllers.Gameplay;
using HighLow.Scripts.Controllers.Stat;
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
        private IStatController _statController;

        [Inject]
        private void Init(IGameLogicController gameLogicController, 
            IGameplayController gameplayController, 
            IInteractiveObjectsManager interactiveObjectsManager,
            IStatController statController)
        {
            _gameLogicController = gameLogicController;
            _gameplayController = gameplayController;
            _interactiveObjectsManager = interactiveObjectsManager;
            _statController = statController;
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
            _statController.UpdateWins(1);
            _statController.UpdateFailures(3);
            _statController.UpdateResponse(5);
            _statController.UpdateBestStreak(10);
            
            if(View != null)
                View.Remove(() =>
                {
                    _interactiveObjectsManager.Instantiate("WinPanel", "PopupsContainer");
                    _interactiveObjectsManager.Instantiate("StatPanel", "StatContainer");
                });

        }

        private void OnGameLost()
        {
            _statController.UpdateWins(555);
            _statController.UpdateFailures(555);
            _statController.UpdateResponse(555);
            _statController.UpdateBestStreak(555);
            
            if(View!= null)
                View.Remove(() =>
                {
                    _interactiveObjectsManager.Instantiate("LostPanel", "PopupsContainer");
                    _interactiveObjectsManager.Instantiate("StatPanel", "StatContainer");
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