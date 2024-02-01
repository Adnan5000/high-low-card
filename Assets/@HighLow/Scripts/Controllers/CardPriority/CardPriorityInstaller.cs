using Zenject;

namespace HighLow.Scripts.Controllers.CardPriority
{
    public class CardPriorityInstaller : Installer<CardPriorityInstaller>
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesTo<CardPriorityController>().AsSingle();
        }
    }
}