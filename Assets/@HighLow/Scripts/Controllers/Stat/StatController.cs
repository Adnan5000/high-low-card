using System.IO;
using HighLow.Scripts.Common;
using Unity.VisualScripting;
using UnityEngine;

namespace HighLow.Scripts.Controllers.Stat
{
    public class StatController : IInitializable ,IStatController
    {
        private StatsInfo StatsInfo { get; set; } = new StatsInfo();
        
        
        public void Initialize()
        {
            LoadData();
        }
        
        public void SaveData()
        {
            DataSaver.SaveData(new StatsWrapper(StatsInfo), GetSavePath());
        }

        public void DeleteData()
        {
            DataSaver.DeleteData(GetSavePath());
        }

        public void LoadData()
        {
            StatsWrapper dataWrapper = DataSaver.LoadData<StatsWrapper>(GetSavePath());
            if (dataWrapper.Stats != null)
            {
                StatsInfo = dataWrapper.Stats;
            }
        }

        private static string GetSavePath(){
            return Path.Combine(Application.persistentDataPath,"StatsInfo.json");
        }

        public void UpdateWins(int value)
        {
            StatsInfo.TotalWins = value;
            SaveData();
        }
        
        public void UpdateFailures(int value)
        {
            StatsInfo.TotalFailures = value;
            SaveData();
        }
        
        public void UpdateResponse(int value)
        {
            StatsInfo.AverageResponse = value;
            SaveData();
        }
        
        public void UpdateBestStreak(int value)
        {
            StatsInfo.BestStreak = value;
            SaveData();
        }
        
        public StatsInfo GetStats()
        {
            LoadData();
            return StatsInfo;
        }

        // public int Wins { get; set; }
        // public int Failures { get; set; }
        // public int AvgTime { get; set; }
        // public int BestTime { get; set; }
    }
    [System.Serializable]
    public class StatsInfo
    {
        public int TotalWins = 0;
        public int TotalFailures = 0;
        public int AverageResponse = 0;
        public int BestStreak = 0;
    }
    
    public struct StatsWrapper
    {
        public StatsInfo Stats;

        public StatsWrapper(StatsInfo data)
        {
            Stats = data;
        }
    }
}