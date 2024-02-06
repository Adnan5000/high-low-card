using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HighLow.Scripts.Caching;
using HighLow.Scripts.Controllers.GameLogic;
using HighLow.Scripts.Controllers.Stat;
using UnityEngine;
using Zenject;

namespace HighLow.Scripts.Controllers.Time
{
    class TimeController : ITimeController
    {
        private float _timer = 0f;
        private bool _isTimerRunning = false;
        private float _choiceTime = 5f;
        private string _choiceTimeText;
        
        private float _averageResponseTime;
        private List<float> _responseTimes = new List<float>();

        public float Timer => _timer;
        public float AverageResponseTime => _averageResponseTime;
        
        public string ChoiceTimeText => _choiceTimeText;
        
        private IGameLogicController _gamelogicController;
        private IStatController _statController;

        [Inject]
        private void Init(IGameLogicController gameLogicController,
            IStatController statController)
        {
            _gamelogicController = gameLogicController;
            _statController = statController;
        }
        
        public async Task StartTimer()
        {
            ResetTimer();
            ResetChoiceTimer();
            
            if (!_isTimerRunning)
            {
                _isTimerRunning = true;
                await TimerRoutine();
            }
        }
        
        private async Task TimerRoutine()
        {
            while (_isTimerRunning)
            {
                _timer += 1f;
                _choiceTime -= 1f;
                _choiceTimeText = FormatTime(_choiceTime);
                if(_choiceTime <= 0)
                {
                    StopTimer();
                    _gamelogicController.GameLost();
                    
                }
                
                await Task.Delay(1000);
            }
        }

        public void StopTimer()
        {
            CalculateAverageResponseTime();

            if (_isTimerRunning)
            {
                _isTimerRunning = false;
            }
        }
        
        public void ResetChoiceTimer()
        {
            if (_timer > 0.1f)
            {
                AddResponseTime();
            }

            _choiceTime = DataProvider.Instance.ChoiceTime;

        }
        
        private void AddResponseTime()
        {
            float tempResponseTime = DataProvider.Instance.ChoiceTime-_choiceTime;
            if(tempResponseTime<0.1f)
                tempResponseTime = 0.1f;
            _responseTimes.Add(tempResponseTime);
        }
        
        public void ResetTimer()
        {
            _responseTimes.Clear();
            _timer = 0f;
        }
        
        public void CalculateAverageResponseTime()
        {
            float sum = 0;
            foreach (var responseTime in _responseTimes)
            {
                sum += responseTime;
            }
            _averageResponseTime = sum / _responseTimes.Count;
            _averageResponseTime = (float)Math.Round(_averageResponseTime, 2);
        }
        
        private string FormatTime(float seconds)
        {
            TimeSpan timeSpan = TimeSpan.FromSeconds(seconds);
            return $"{(int)timeSpan.TotalMinutes:D2}:{timeSpan.Seconds:D2}";
        }
    }
}