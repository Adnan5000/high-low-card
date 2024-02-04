using Arch.Views.Mediation;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

namespace HighLow.Scripts.Views.ResultPanels.StatPanel
{
    class StatPanelView : View, IStatPanelView
    {
        [Header("Texts")]
        [SerializeField] private TMP_Text wins;
        [SerializeField] private TMP_Text failures;
        [SerializeField] private TMP_Text avgTime;
        [SerializeField] private TMP_Text bestTime;
        
        public void SetWins(int value)
        {
            wins.text = value.ToString();
        }
        
        public void SetFailures(int value)
        {
            failures.text = value.ToString();
        }
        
        public void SetAvgTime(float value)
        {
            avgTime.text = value.ToString("F2");
        }
        
        public void SetBestTime(float value)
        {
            bestTime.text = value.ToString("F2");
        }
    }
}