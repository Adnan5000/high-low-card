using System;
using Arch.SoundManager;
using Arch.Views.Mediation;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace HighLow.Scripts.Views.MenuPanel
{
    public class MenuPanelView: View, IMenuPanelView
    {
        public Action PlayButtonClicked { get; set; }
        public Action ResetButtonClicked { get; set; }
        
        [Header("Buttons")]
        [SerializeField] private Button btnPlay;
        [SerializeField] private Button btnReset;
        
        [Inject] private ISoundManager _soundManager;

        private void Start()
        {
            btnPlay.onClick.AddListener(ClickToPlay);
            btnReset.onClick.AddListener(ClickToReset);
        }

        private void ClickToReset()
        {
            AL_HapticFeedBack.Generate(HapticTypes.LightImpact);
            _soundManager.PlayAudioClip(new AudioClipManagerModel()
            {
                ClipName = "Click"
            });
            
            ResetButtonClicked?.Invoke();
        }

        private void ClickToPlay()
        {
            AL_HapticFeedBack.Generate(HapticTypes.LightImpact);
            _soundManager.PlayAudioClip(new AudioClipManagerModel()
            {
                ClipName = "Click"
            });
            
            PlayButtonClicked?.Invoke();
        }
    }
}