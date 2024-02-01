namespace HighLow.Scripts.Controllers.Gameplay
{
    public interface IGameplayController
    {
        public void InitCards();
        public void MoveToNextCard();
        public bool IsFinalCard();
    }
}