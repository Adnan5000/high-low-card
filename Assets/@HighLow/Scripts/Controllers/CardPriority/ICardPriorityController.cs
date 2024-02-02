using System.Collections.Generic;

namespace HighLow.Scripts.Controllers.CardPriority
{
    public interface ICardPriorityController
    {
        public void Init();
        ushort GetPriorityValue(string cardType);
    }
}