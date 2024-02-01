using System;
using Arch.Views.Mediation;
using UnityEngine;
using UnityEngine.UI;

namespace HighLow.Scripts.Views.GameplayPanel
{
    public class GameplayPanelView: View, IGameplayPanel
    {
        public Action HighButtonClicked { get; set; }
        public Action LowButtonClicked { get; set; }
        public Action EqualButtonClicked { get; set; }
        
        [Header("Buttons")]
        [SerializeField] private Button btnHigh;
        [SerializeField] private Button btnLow;
        [SerializeField] private Button btnEqual;

        private void Start()
        {
            btnHigh.onClick.AddListener(ClickToHigh);
            btnLow.onClick.AddListener(ClickToLow);
            btnEqual.onClick.AddListener(ClickToEqual);
        }

        private void ClickToHigh()
        {
            HighButtonClicked?.Invoke();
        }
        
        private void ClickToLow()
        {
            LowButtonClicked?.Invoke();
        }
        
        private void ClickToEqual()
        {
            EqualButtonClicked?.Invoke();
        }
    }
}