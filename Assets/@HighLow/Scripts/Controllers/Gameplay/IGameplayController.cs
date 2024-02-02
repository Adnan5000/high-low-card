using System.Collections.Generic;
using HighLow.Scripts.Views.Card;

namespace HighLow.Scripts.Controllers.Gameplay
{
    public interface IGameplayController
    {
        public void InitCards(ushort maxCount);
        public void MoveToNextCard();
        public bool IsFinalCard();
        
        public List<CardView> CardViews { get; set; }
        public string GetCardId();
    }
}