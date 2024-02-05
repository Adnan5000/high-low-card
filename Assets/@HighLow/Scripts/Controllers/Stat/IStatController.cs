namespace HighLow.Scripts.Controllers.Stat
{
    public interface IStatController
    {
        public void Initialize();
        public void UpdateWins();
        public void UpdateFailures();
        public void UpdateResponse(int value);
        public void UpdateBestStreak(int value);
        public StatsInfo GetStats();
        public void DeleteData();
    }
}