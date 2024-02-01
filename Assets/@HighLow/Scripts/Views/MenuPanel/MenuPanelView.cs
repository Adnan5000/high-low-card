using System;
using Arch.Views.Mediation;
using UnityEngine;
using UnityEngine.UI;

namespace HighLow.Scripts.Views.MenuPanel
{
    public class MenuPanelView: View, IMenuPanelView
    {
        public Action PlayButtonClicked { get; set; }
        
        [Header("Buttons")]
        [SerializeField] private Button btnPlay;

        private void Start()
        {
            btnPlay.onClick.AddListener(ClickToPlay);
        }

        private void ClickToPlay()
        {
            PlayButtonClicked?.Invoke();
        }
    }
}