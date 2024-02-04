using Arch.Views.Mediation;
using HighLow.Scripts.Controllers.Stat;
using Zenject;

namespace HighLow.Scripts.Views.ResultPanels.StatPanel
{
    public class StatPanelMediator : Mediator<IStatPanelView>
    {
        private IStatController _statController;

        [Inject]
        private void Init(IStatController statController)
        {
            _statController = statController;

            ShowStats();
        }

        private void ShowStats()
        {
            StatsInfo statsInfo = _statController.GetStats(); 
            
            View.SetWins(statsInfo.TotalWins);
            View.SetFailures(statsInfo.TotalFailures);
            View.SetAvgTime(statsInfo.AverageResponse);
            View.SetBestTime(statsInfo.BestStreak);
        }
    }
}