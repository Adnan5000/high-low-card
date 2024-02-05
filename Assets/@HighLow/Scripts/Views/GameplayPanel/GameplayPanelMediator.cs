using System.Threading.Tasks;
using Arch.InteractiveObjectsSpawnerService;
using Arch.Views.Mediation;
using HighLow.Scripts.Common;
using HighLow.Scripts.Controllers.GameLogic;
using HighLow.Scripts.Controllers.Gameplay;
using HighLow.Scripts.Controllers.Stat;
using HighLow.Scripts.Controllers.Time;
using Zenject;

namespace HighLow.Scripts.Views.GameplayPanel
{
    public class GameplayPanelMediator: Mediator<IGameplayPanel>
    {
        private IGameLogicController _gameLogicController;
        private IGameplayController _gameplayController;
        private IInteractiveObjectsManager _interactiveObjectsManager;
        private IStatController _statController;
        private ITimeController _timeController;

        [Inject]
        private void Init(IGameLogicController gameLogicController, 
            IGameplayController gameplayController, 
            IInteractiveObjectsManager interactiveObjectsManager,
            IStatController statController,
            ITimeController timeController)
        {
            _gameLogicController = gameLogicController;
            _gameplayController = gameplayController;
            _interactiveObjectsManager = interactiveObjectsManager;
            _statController = statController;
            _timeController = timeController;
        }

        protected override void OnMediatorInitialize()
        {
            base.OnMediatorInitialize();
         
            View.HighButtonClicked += OnHigh;
            View.LowButtonClicked += OnLow;
            View.EqualButtonClicked += OnEqual;
            
            _gameLogicController.GameLost += OnGameLost;
            _gameLogicController.GameWin += OnGameWin;

            _statController.Initialize();
            
            _timeController.StartTimer();
            ShowTime();
            
        }

        protected override void OnMediatorDispose()
        {
            _gameLogicController.GameLost -= OnGameLost;
            _gameLogicController.GameWin -= OnGameWin;
        }

        private async Task ShowTime()
        {
            while (true)
            {
                View.TxtChoiceTime.text = _timeController.ChoiceTimeText;
                await Task.Delay(5);
            }
        }

        private void OnGameWin()
        {
            _timeController.StopTimer();
            
            _statController.CheckAndSetBestStreak();
            
            _statController.UpdateWins();
            
            _statController.UpdateResponse(5);
            
            if(View != null)
                View.Remove(() =>
                {
                    _interactiveObjectsManager.Instantiate("WinPanel", "PopupsContainer");
                    _interactiveObjectsManager.Instantiate("StatPanel", "StatContainer");
                });
        }

        private void OnGameLost()
        {
            _timeController.StopTimer();
            
            _statController.UpdateFailures();
            
            _statController.UpdateResponse(555);
            
            if(View!= null)
                View.Remove(() =>
                {
                    _interactiveObjectsManager.Instantiate("LostPanel", "PopupsContainer");
                    _interactiveObjectsManager.Instantiate("StatPanel", "StatContainer");
                });
        }

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