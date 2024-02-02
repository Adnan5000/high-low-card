using System;
using HighLow.Scripts.Common;

namespace HighLow.Scripts.Controllers.GameLogic
{
    public interface IGameLogicController
    {
        public Action GameWin{ get; set; }
        public Action GameLost { get; set; }
        public void SetPreviousCardPriorityValue(ushort value);
        public void CheckMove(string cardId, EnumsHandler.Moves moveType);
    }
}