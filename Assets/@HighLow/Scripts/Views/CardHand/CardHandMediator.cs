using Arch.InteractiveObjectsSpawnerService;
using Arch.Views.Mediation;
using HighLow.Scripts.Caching;
using HighLow.Scripts.Controllers.GameLogic;
using UnityEngine;
using Zenject;

namespace HighLow.Scripts.Views.CardHand
{
    public class CardHandMediator: Mediator<ICardHandView>
    {
        protected override void OnMediatorInitialize()
        {
            base.OnMediatorInitialize();
         
            View.HandOutCards += OnHandOutCards;
        }

        private void OnHandOutCards()
        {
            DataProvider.Instance.deck.ShuffleDeck();
            for (int i = 0; i < View.MaxCount; i++)
            {
                GameObject card = DataProvider.Instance.deck.GetCard().gameObject;
                card.transform.position = View.CardPositions[i].position;
            }
        }
    }
}