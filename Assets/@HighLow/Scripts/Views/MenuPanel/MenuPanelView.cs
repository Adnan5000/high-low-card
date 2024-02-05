using System;
using Arch.Views.Mediation;
using UnityEngine;
using UnityEngine.UI;

namespace HighLow.Scripts.Views.MenuPanel
{
    public class MenuPanelView: View, IMenuPanelView
    {
        public Action PlayButtonClicked { get; set; }
        public Action ResetButtonClicked { get; set; }
        
        [Header("Buttons")]
        [SerializeField] private Button btnPlay;
        [SerializeField] private Button btnReset;

        private void Start()
        {
            btnPlay.onClick.AddListener(ClickToPlay);
            btnReset.onClick.AddListener(ClickToReset);
        }

        private void ClickToReset()
        {
            ResetButtonClicked?.Invoke();
        }

        private void ClickToPlay()
        {
            PlayButtonClicked?.Invoke();
        }
    }
}