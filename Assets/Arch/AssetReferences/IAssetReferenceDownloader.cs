using System;
using UnityEngine;
using UnityEngine.U2D;
using Object = UnityEngine.Object;

namespace Arch.AssetReferences
{
    public interface IAssetReferenceDownloader
    {
        public void SpawnScriptableById(string id, Action<ScriptableObject> callback);
        public bool AllUploded { get; }
        public int WorkersCount{ get; }
        public void SpawnById(string id, Action<GameObject> callback);
        public void SpawnSpriteById(string id, Action<Sprite> callback);
        public void SpawnObjectByIdAndDispose(string id, Action<Object> callback);
        public void CheckPreloadUpdate(Action<bool> callbackResult);
        public void SpawnUnknownById(string id,
            Action<Sprite> callbackSprite,
            Action<GameObject> callbackGameObject,
            Action<SpriteAtlas> callbackSpriteAtlas = null);

        public void DownloadAudioById(string id, Action<AudioClip> callback);
    }
}