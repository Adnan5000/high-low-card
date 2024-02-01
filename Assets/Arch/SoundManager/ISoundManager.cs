using Arch.SoundManager.Config;
using UnityEngine;
using UnityEngine.Audio;

namespace Arch.SoundManager
{
    public interface ISoundManager
    {
        public string CurrentMusicClip { get; }
        public AudioSource MusicSource { set; }
        public AudioMixer MasterMixer { set; }
        public SoundsConfig SoundConfig { set; }
        public bool SoundOn { get; set; }
        public bool MusicOn { get; set; }
        public float SoundVolume { get; set; }
        public float MusicVolume  { get; set; }

        public void PlayAudioClip(AudioClipManagerModel clipInfo);
        public void StopPlayAudio(AudioSource clipSource);

    }
}