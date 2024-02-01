using HighLow.Scripts.Common;

namespace HighLow.Scripts.Controllers.GameLogic
{
    public interface IGameLogicController
    {
        public void CheckMove(string cardId, EnumsHandler.Moves moveType);
    }
}