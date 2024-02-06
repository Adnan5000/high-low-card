using System;
using Arch.SoundManager;
using Arch.Views.Mediation;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Zenject;

namespace HighLow.Scripts.Views.ResultPanels.LostPanel
{
    class LostPanelView : View, ILostPanelView
    {
        public Action PlayAgainButtonClicked { get; set; }
        
        [FormerlySerializedAs("btnPlay")]
        [Header("Buttons")]
        [SerializeField] private Button btnPlayAgain;

        [Inject] private ISoundManager _soundManager;
        
        private void Start()
        {
            btnPlayAgain.onClick.AddListener(ClickToPlay);
        }

        private void ClickToPlay()
        {
            AL_HapticFeedBack.Generate(HapticTypes.LightImpact);
            _soundManager.PlayAudioClip(new AudioClipManagerModel()
            {
                ClipName = "Click"
            });
            
            PlayAgainButtonClicked?.Invoke();
        }
    }
}