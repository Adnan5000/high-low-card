using Arch.AssetReferences;
using Arch.Installers;
using Arch.InteractiveObjectsSpawnerService;
using Arch.Signals;
using Arch.SoundManager;
using HighLow.Scripts.Controllers.CardPriority;
using HighLow.Scripts.Controllers.GameLogic;
using HighLow.Scripts.Views.Installers;
using Zenject;

namespace DI.Resources
{
    public class CommonInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            MessageBrokerInstaller.Install(Container);
            SignalsInstaller.Install(Container);
            AssetReferenceInstaller.Install(Container);
            InteractiveObjectServiceInstaller.Install(Container);
            SoundManagerInstaller.Install(Container);
            
            MenuPanelInstaller.Install(Container);
            
            CardPriorityInstaller.Install(Container);
            GameLogicInstaller.Install(Container);
            GameplayPanelInstaller.Install(Container);
            CardHandInstaller.Install(Container);
            CardInstaller.Install(Container);
            
        }
    }
}