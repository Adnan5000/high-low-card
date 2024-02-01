using System;
using Arch.InteractiveObjectsSpawnerService;
using HighLow.Scripts.Common;
using HighLow.Scripts.Controllers.CardPriority;
using HighLow.Scripts.Controllers.Gameplay;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;
using Zenject;

namespace HighLow.Scripts.Controllers.GameLogic
{
    class GameLogicController : IGameLogicController
    {
        public Action GameWin{ get; set; }
        public Action GameLost { get; set; }
        
        private ICardPriorityController _cardPriorityController;
        private IGameplayController _gameplayController;
        
        private ushort _previousCardPriorityValue;

        [Inject]
        private void Init(ICardPriorityController cardPriorityController, IGameplayController gameplayController)
        {
            _cardPriorityController = cardPriorityController;
            _gameplayController = gameplayController;
        }

        public void SetPreviousCardPriorityValue(ushort value)
        {
            _previousCardPriorityValue = value;
        }
        
        public void CheckMove(string cardId, EnumsHandler.Moves moveType)
        {
            var currentCardPriorityValue = _cardPriorityController.GetPriorityValue(cardId);

            switch (moveType)
            {
                case EnumsHandler.Moves.High:
                    if (currentCardPriorityValue > _previousCardPriorityValue)
                    {
                        //GameWin?.Invoke();
                        if (!_gameplayController.IsFinalCard())
                        {
                            _gameplayController.MoveToNextCard();
                        }
                        else
                        {
                            GameLost?.Invoke();
                        }
                    }
                    else
                    {
                        GameLost?.Invoke();
                    }
                    break;
                case EnumsHandler.Moves.Low:
                    if (currentCardPriorityValue < _previousCardPriorityValue)
                    {
                        //GameWin?.Invoke();
                        if (!_gameplayController.IsFinalCard())
                        {
                            _gameplayController.MoveToNextCard();
                        }
                        else
                        {
                            GameLost?.Invoke();
                        }
                    }
                    else
                    {
                        GameLost?.Invoke();
                    }
                    break;
                case EnumsHandler.Moves.Equal:
                    if (currentCardPriorityValue == _previousCardPriorityValue)
                    {
                        GameWin?.Invoke();
                    }
                    else
                    {
                        GameLost?.Invoke();
                    }
                    break;
            }
            
            
            
            
            
            if (currentCardPriorityValue > _previousCardPriorityValue)
            {
                //GameWin?.Invoke();
            }
            else
            {
                //GameLost?.Invoke();
            }
        }
    }
}