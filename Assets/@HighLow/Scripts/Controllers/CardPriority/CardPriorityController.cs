using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace HighLow.Scripts.Controllers.CardPriority
{
    public class CardPriorityController: ICardPriorityController
    {
        private Dictionary<string, ushort> _cardPriorities;

        public void Init()
        {
            _cardPriorities = new Dictionary<string, ushort>
            {
                {"2", 2},
                {"3", 3},
                {"4", 4},
                {"5", 5},
                {"6", 6},
                {"7", 7},
                {"8", 8},
                {"9", 9},
                {"10", 10},
                {"J", 11},
                {"Q", 12},
                {"K", 13},
                {"A", 14}
            };
        }

        public ushort GetPriorityValue(string cardType)
        {
            if (_cardPriorities.ContainsKey(cardType))
            {
                return _cardPriorities[cardType];
            }
            else
            {
                Debug.LogError("Card type not found in dictionary: " + cardType);
                return 666;
            }
        }
    }
}
