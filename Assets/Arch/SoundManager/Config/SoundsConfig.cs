using UnityEngine;

namespace Arch.SoundManager.Config
{
    [CreateAssetMenu(fileName = "Sounds Config", menuName = "Configs/Audio")]
    public class SoundsConfig: ScriptableObject {
        [SerializeField]
        public ClipsDictionary ClipsDictionary = new ClipsDictionary();

    }
}