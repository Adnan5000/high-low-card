using Arch.InteractiveObjectsSpawnerService;
using Arch.Views.Mediation;
using HighLow.Scripts.Caching;
using HighLow.Scripts.Controllers.CardPriority;
using HighLow.Scripts.Controllers.GameLogic;
using HighLow.Scripts.Controllers.Gameplay;
using HighLow.Scripts.Views.Card;
using UnityEngine;
using Zenject;

namespace HighLow.Scripts.Views.CardHand
{
    public class CardHandMediator: Mediator<ICardHandView>
    {
        private IGameplayController _gameplayController;
        private ICardPriorityController _cardPriorityController;

        [Inject]
        private void Init(
            IGameplayController gameplayController, ICardPriorityController cardPriorityController)
        {
            _gameplayController = gameplayController;
            _cardPriorityController = cardPriorityController;
        }

        protected override void OnMediatorInitialize()
        {
            base.OnMediatorInitialize();

            View.HandOutCards += OnHandOutCards;
        }
        
        private void OnHandOutCards()
        {
            _cardPriorityController.Init();
            
            DataProvider.Instance.deck.ShuffleDeck();
            
            for (int i = 0; i < View.MaxCount; i++)
            {
                GameObject card = DataProvider.Instance.deck.GetCard().gameObject;
                card = Object.Instantiate(card, View.CardPositions[i].position, Quaternion.identity);
                card.transform.SetParent(View.CardPositions[i]);
                _gameplayController.CardViews.Add(card.GetComponent<CardView>());
                //rotate the card to face the player - Y axis
                card.transform.Rotate(0, 180, 0);
            }
            
            _gameplayController.InitCards(View.MaxCount);
            //TODO: Animate cards to their positions one by one
        }
    }
}