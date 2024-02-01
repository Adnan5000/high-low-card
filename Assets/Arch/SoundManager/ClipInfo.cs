using System;
using UnityEngine;
using UnityEngine.Audio;

namespace Arch.SoundManager
{
    public class ClipInfo {
        public AudioClip Clip;
        public float Volume;
        public AudioMixerGroup OutputAudioMixerGroup;
        public Action<AudioSource> SourceCallback;
        public int MaxSimultaneouslyCalls = 4;
    }
}