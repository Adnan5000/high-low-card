using Arch.Views.Mediation;
using UnityEngine;

namespace HighLow.Scripts.Views.Card
{
    public class CardView: View, ICardView
    {
        [SerializeField] private string cardId;
    }
}