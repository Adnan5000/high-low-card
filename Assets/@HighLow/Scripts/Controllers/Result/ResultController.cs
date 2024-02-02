using Arch.InteractiveObjectsSpawnerService;
using HighLow.Scripts.Controllers.CardPriority;
using HighLow.Scripts.Controllers.GameLogic;
using Zenject;

namespace HighLow.Scripts.Controllers.Result
{
    class ResultController : IResultController
    {
        private IGameLogicController _gamelogicController;
        private IInteractiveObjectsManager _interactiveObjectsManager;

        [Inject]
        private void Init(IGameLogicController gameLogicController, IInteractiveObjectsManager interactiveObjectsManager)
        {
            _gamelogicController = gameLogicController;
            _interactiveObjectsManager = interactiveObjectsManager;
            
            _gamelogicController.GameLost += OnGameLost;
            _gamelogicController.GameWin += OnGameWin;
        }

        private void OnGameWin()
        {
            //_interactiveObjectsManager.Instantiate("WinPanel", "GamplayUIContainer");
        }
        
        private void OnGameLost()
        {
            //_interactiveObjectsManager.Instantiate("LostPanel", "GamplayUIContainer");
        }
    }
}