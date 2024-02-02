﻿using System;
using Arch.Views.Mediation;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace HighLow.Scripts.Views.ResultPanels.LostPanel
{
    class LostPanelView : View, ILostPanelView
    {
        public Action PlayAgainButtonClicked { get; set; }
        
        [FormerlySerializedAs("btnPlay")]
        [Header("Buttons")]
        [SerializeField] private Button btnPlayAgain;

        private void Start()
        {
            btnPlayAgain.onClick.AddListener(ClickToPlay);
        }

        private void ClickToPlay()
        {
            PlayAgainButtonClicked?.Invoke();
        }
    }
}