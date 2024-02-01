using System;
using Arch.SoundManager.Config;
using UnityEngine;

namespace Arch.SoundManager
{
    public class AudioClipManagerModel
    {
        public string ClipName;
        public SoundType ClipType = SoundType.Default;
        public float Delay = 0;
        public float Pitch = 1;
        public float PitchTime = 0;
        public float Volume = 1;
        public Action<AudioSource> Source;
    }
}