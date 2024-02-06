using System.Collections.Generic;
using HighLow.Scripts.Controllers.CardPriority;
using HighLow.Scripts.Controllers.GameLogic;
using HighLow.Scripts.Controllers.Time;
using HighLow.Scripts.Views.Card;
using DG.Tweening;
using HighLow.Scripts.Caching;
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
        private ITimeController _timeController;

        [Inject]
        private void Init(IGameLogicController gameLogicController, 
            ICardPriorityController cardPriorityController,
            ITimeController timeController)
        {
            _gamelogicController = gameLogicController;
            _cardPriorityController = cardPriorityController;
            _timeController = timeController;
        }
        
        public List<CardView> CardViews
        {
            get => _cardViews;
            set => _cardViews = value;
        }

        public void InitCards(ushort maxCount)
        {
            _cardViews[0].TurnCardFrontFace();
            _currentCardIndex = 1;
            _maxCount = maxCount;
            
            _gamelogicController.SetPreviousCardPriorityValue(_cardPriorityController.GetPriorityValue(_cardViews[_currentCardIndex-1].GetCardId()));
            Debug.Log("card index: "+ _currentCardIndex+ " max count: "+ _maxCount);
        }


        public void MoveToNextCard()
        {
            ShowCardClone();
            _timeController.ResetChoiceTimer();
            
            _cardViews[_currentCardIndex].TurnCardFrontFace();
            
            _currentCardIndex++;
            _gamelogicController.SetPreviousCardPriorityValue(_cardPriorityController.GetPriorityValue(_cardViews[_currentCardIndex-1].GetCardId()));

            Debug.Log("card index: "+ _currentCardIndex+ " max count: "+ _maxCount);

        }
        
        public void ShowCardClone()
        {
            CardView cardView = _cardViews[_currentCardIndex];
            CardView cardObj = Object.Instantiate(cardView, cardView.transform.position, cardView.transform.rotation);
            if (DataProvider.Instance.ShowCardsForDebug)
            {
                cardObj.GetGameObject.gameObject.transform.DORotate(new Vector3(0, 180, 0), 0.25f);
            }

            cardObj.GetGameObject.gameObject.transform.DOScale(new Vector3(4, 4, 4),0.25f);
            cardObj.GetGameObject.gameObject.transform.DOMove(new Vector3(0, 0, -0.05f), 1f).OnComplete(() =>
            {
                AL_HapticFeedBack.Generate(HapticTypes.LightImpact);
                cardObj.GetGameObject.gameObject.transform.DOMove(new Vector3(10, 0, 0), 2f);
            });
        }

        public bool IsFinalCard()
        {
            if (_currentCardIndex == _maxCount - 1)
            {
                ShowCardClone();
                return true;
                
            }

            return false;
        }
        
        public string GetCardId()
        {
            Debug.Log("Card Id: "+ _cardViews[_currentCardIndex].GetCardId());
            return _cardViews[_currentCardIndex].GetCardId();
        }
    }
}