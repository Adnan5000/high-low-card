using System.Collections.Generic;

namespace HighLow.Scripts.Controllers.CardPriority
{
    public interface ICardPriorityController
    {
        int GetPriorityValue(string cardType);
    }
}