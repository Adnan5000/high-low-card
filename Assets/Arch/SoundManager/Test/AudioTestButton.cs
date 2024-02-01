using UnityEngine;
using Zenject;

namespace Arch.SoundManager.Test
{
    public class AudioTestButton : MonoBehaviour
    {
        [Inject] private ISoundManager _soundManager;

        public void OnClick()
        {
            _soundManager.PlayAudioClip(new AudioClipManagerModel()
            {
                ClipName = "Logo2"
            });
        }
    }
}