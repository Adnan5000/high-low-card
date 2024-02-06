using System.IO;
using HighLow.Scripts.Common;
using HighLow.Scripts.Controllers.GameLogic;
using HighLow.Scripts.Controllers.Time;
using UnityEngine;
using Zenject;
using IInitializable = Unity.VisualScripting.IInitializable;

namespace HighLow.Scripts.Controllers.Stat
{
    public class StatController : IInitializable ,IStatController
    {
        private ITimeController _timeController;
        private StatsInfo StatsInfo { get; set; } = new StatsInfo();
        
        [Inject]
        private void Init(ITimeController timeController)
        {
            _timeController = timeController;
        }
        
        public void Initialize()
        {
            Debug.Log("Stat controller initialized");
            LoadData();
        }
        
        public void SaveData()
        {
            DataSaver.SaveData(new StatsWrapper(StatsInfo), GetSavePath());
        }

        public void DeleteData()
        {
            DataSaver.DeleteData(GetSavePath());
            StatsInfo = new StatsInfo();
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

        public void UpdateWins()
        {
            StatsInfo.TotalWins++;
            SaveData();
        }
        
        public void UpdateFailures()
        {
            StatsInfo.TotalFailures++;
            SaveData();
        }
        
        public void UpdateResponse()
        {
            StatsInfo.AverageResponse = _timeController.AverageResponseTime;
            SaveData();
        }
        
        private void UpdateBestStreak(int value)
        {
            StatsInfo.BestStreak = value;
            SaveData();
        }
        
        public StatsInfo GetStats()
        {
            LoadData();
            return StatsInfo;
        }
        
        public void CheckAndSetBestStreak()
        {
            if (_timeController.Timer < GetStats().BestStreak)
            {
                UpdateBestStreak((int)_timeController.Timer);
            }
        }
    }
    
    [System.Serializable]
    public class StatsInfo
    {
        public int TotalWins = 0;
        public int TotalFailures = 0;
        public float AverageResponse = 0;
        public float BestStreak = 999;
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