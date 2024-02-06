using System;
using System.Collections.Generic;
using System.Threading.Tasks;
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
                
                Debug.Log("Timer: " + _timer);
                await Task.Delay(1000);
            }
        }

        public void StopTimer()
        {
            if (_isTimerRunning)
            {
                _isTimerRunning = false;
                Debug.Log("Timer Stopped. Final value: " + _timer);
            }
        }
        
        public void ResetChoiceTimer()
        {
            _responseTimes.Add(_choiceTime);
            _choiceTime = 5f;
        }
        
        public void ResetTimer()
        {
            CalculateAverageResponseTime();
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