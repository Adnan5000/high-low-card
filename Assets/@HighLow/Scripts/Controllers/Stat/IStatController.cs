namespace HighLow.Scripts.Controllers.Stat
{
    public interface IStatController
    {
        public void UpdateWins(int value);
        public void UpdateFailures(int value);
        public void UpdateResponse(int value);
        public void UpdateBestStreak(int value);
        public StatsInfo GetStats();
    }
}