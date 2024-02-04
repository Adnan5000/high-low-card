using Arch.Views.Mediation;

namespace HighLow.Scripts.Views.ResultPanels.StatPanel
{
    public interface IStatPanelView: IView
    {
        public void SetWins(int wins);
        public void SetFailures(int failures);
        public void SetAvgTime(float avgTime);
        public void SetBestTime(float bestTime);
    }
}