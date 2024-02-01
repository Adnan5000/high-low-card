using System;
using RotaryHeart.Lib.SerializableDictionary;

namespace Arch.SoundManager.Config
{
    
    [Serializable]
    public class ClipsDictionary : SerializableDictionaryBase<string, AudioClipConfig> { }
}