using System.Collections.Generic;
using HighLow.Scripts.Controllers.CardPriority;
using HighLow.Scripts.Controllers.GameLogic;
using HighLow.Scripts.Views.Card;
using HighLow.Scripts.Views.CardHand;
using UnityEngine;
using Zenject;

namespace HighLow.Scripts.Controllers.Gameplay
{
    public class GameplayController: IGameplayController
    {
        private ushort _currentCardIndex;
        private ushort _maxCount;
        private List<CardView> _cardViews = new List<CardView>();
        private IGameLogicController _gamelogicController;
        private ICardPriorityController _cardPriorityController;

        [Inject]
        private void Init(IGameLogicController gameLogicController, ICardPriorityController cardPriorityController)
        {
            _gamelogicController = gameLogicController;
            _cardPriorityController = cardPriorityController;
        }
        
        public List<CardView> CardViews
        {
            get => _cardViews;
            set => _cardViews = value;
        }

        public void InitCards(ushort maxCount)
        {
            _currentCardIndex = 1;
            _maxCount = maxCount;
            
            _gamelogicController.SetPreviousCardPriorityValue(_cardPriorityController.GetPriorityValue(_cardViews[_currentCardIndex-1].GetCardId()));
            Debug.Log("card index: "+ _currentCardIndex+ " max count: "+ _maxCount);
        }


        public void MoveToNextCard()
        {
            _currentCardIndex++;
            _gamelogicController.SetPreviousCardPriorityValue(_cardPriorityController.GetPriorityValue(_cardViews[_currentCardIndex-1].GetCardId()));

            Debug.Log("card index: "+ _currentCardIndex+ " max count: "+ _maxCount);

        }

        public bool IsFinalCard()
        {
            return _currentCardIndex == _maxCount - 1;
        }
        
        public string GetCardId()
        {
            Debug.Log("Card Id: "+ _cardViews[_currentCardIndex].GetCardId());
            return _cardViews[_currentCardIndex].GetCardId();
        }
    }
}