using Arch.Views.Mediation;
using DG.Tweening;
using UnityEngine;

namespace HighLow.Scripts.Views.Card
{
    public class CardView: View, ICardView
    {
        [SerializeField] private string cardId;
        
        public string GetCardId()
        {
            return cardId;
        }

        public void TurnCardFrontFace()
        {
            transform.DORotate(new Vector3(0, 180, 0), 0.5f).SetEase(Ease.Linear).OnComplete(() =>
            {
                Debug.Log("Card flipped!");
            });
        }
    }
}