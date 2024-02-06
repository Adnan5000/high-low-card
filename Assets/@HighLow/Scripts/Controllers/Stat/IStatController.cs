namespace HighLow.Scripts.Controllers.Stat
{
    public interface IStatController
    {
        public void Initialize();
        public void UpdateWins();
        public void UpdateFailures();
        public void UpdateResponse();
        public void CheckAndSetBestStreak();
        public StatsInfo GetStats();
        public void DeleteData();
    }
}