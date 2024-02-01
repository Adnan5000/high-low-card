using Arch.Views.Mediation;
using HighLow.Scripts.Views.Card;
using HighLow.Scripts.Views.CardHand;
using Zenject;

namespace HighLow.Scripts.Views.Installers
{
    public class CardInstaller : Installer<CardInstaller>
    {
        public override void InstallBindings()
        {
            Container.BindViewToMediator<CardView, CardMediator>();
        }
    }
}