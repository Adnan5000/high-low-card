using Arch.Views.Mediation;

namespace HighLow.Scripts.Views.Card
{
    public interface ICardView: IView
    {
        public string GetCardId();
        public void TurnCardFrontFace();
    }
}