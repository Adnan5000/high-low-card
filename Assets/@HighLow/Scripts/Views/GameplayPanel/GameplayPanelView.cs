using System;
using System.Threading.Tasks;
using Arch.Views.Mediation;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
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

        [Header("Texts")]
        [SerializeField] private TMP_Text txtChoiceTime;
        public TMP_Text TxtChoiceTime => txtChoiceTime;

        
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