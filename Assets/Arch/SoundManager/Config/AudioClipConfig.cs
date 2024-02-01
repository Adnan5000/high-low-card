using System;
using UnityEngine;
using UnityEngine.Audio;

namespace Arch.SoundManager.Config
{
    [Serializable]
    public class AudioClipConfig 
    {
        public AudioClip Reference;
        public float Volume = 1;
        public AudioMixerGroup OutputAudioMixerGroup;
        public int MaxSimultaneouslyCalls = 1;
        
    }
}