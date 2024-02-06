using Arch.InteractiveObjectsSpawnerService;
using Arch.SoundManager;
using Arch.Views.Mediation;
using HighLow.Scripts.Caching;
using HighLow.Scripts.Controllers.CardPriority;
using HighLow.Scripts.Controllers.GameLogic;
using HighLow.Scripts.Controllers.Gameplay;
using HighLow.Scripts.Views.Card;
using DG.Tweening;
using UnityEngine;
using Zenject;

namespace HighLow.Scripts.Views.CardHand
{
    public class CardHandMediator: Mediator<ICardHandView>
    {
        private IGameplayController _gameplayController;
        private ICardPriorityController _cardPriorityController;
        private IGameLogicController _gameLogicController;
        [Inject] private ISoundManager _soundManager;

        [Inject]
        private void Init(
            IGameplayController gameplayController, ICardPriorityController cardPriorityController, IGameLogicController gameLogicController)
        {
            _gameplayController = gameplayController;
            _cardPriorityController = cardPriorityController;
            _gameLogicController = gameLogicController;
        }

        protected override void OnMediatorInitialize()
        {
            base.OnMediatorInitialize();

            View.HandOutCards += OnHandOutCards;
            
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
            View.Remove(null);
        }

        private void OnGameLost()
        {
            View.Remove(null);

        }

        private void OnHandOutCards()
        {
            _cardPriorityController.Init();
            
            DataProvider.Instance.deck.ShuffleDeck();
            _gameplayController.CardViews.Clear();

            for (int i = 0; i < View.MaxCount; i++)
            {
                GameObject card = DataProvider.Instance.deck.GetCard().gameObject;
                card = Object.Instantiate(card, /*card.transform.position*/new Vector3(0,0,0), Quaternion.identity);
                card.transform.DOMove(View.CardPositions[i].position, 0.5f).OnComplete(()=>
                {
                });
                card.transform.SetParent(View.CardPositions[i]);
                card.transform.SetParent(View.CardPositions[i]);
                _gameplayController.CardViews.Add(card.GetComponent<CardView>());

                if (DataProvider.Instance.ShowCardsForDebug)
                {
                    card.transform.Rotate(0, 180, 0);
                }
            }
            
            _soundManager.PlayAudioClip(new AudioClipManagerModel()
            {
                ClipName = "CardFlip"
            });
            
            _gameplayController.InitCards(View.MaxCount);
        }
    }
}