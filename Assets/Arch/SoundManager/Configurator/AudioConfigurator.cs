using Arch.Signals;
using Arch.SoundManager.Config;
using UnityEngine;
using UnityEngine.Audio;
using Zenject;

namespace Arch.SoundManager.Configurator
{
    public class AudioConfigurator : MonoBehaviour
    {
        [SerializeField] private AudioSource musicSource;
        [SerializeField] private AudioMixer masterMixer;
        [SerializeField] private SoundsConfig soundConfig;

        private ISignalService _signalService;
        
        [Inject]
        public void Init(ISoundManager soundController, ISignalService signalService)
        {
            _signalService = signalService;
            soundController.MusicSource = musicSource;
            soundController.MasterMixer = masterMixer;
            soundController.SoundConfig = soundConfig;
            
            DontDestroyOnLoad(musicSource);
        }
    }
}