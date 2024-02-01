using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.UI;
using Zenject;

namespace Arch.AssetReferences
{
    public class AssetReferenceUnknown : MonoBehaviour
    {
        [SerializeField] private Image image;
        [Header("Check flag if wait image, for safe load.")]
        [SerializeField] private bool waitImage = false;

        private IAssetReferenceDownloader _assetReferenceDownloader;
        
        private void SetInject()
        {
            if(image != null)image.gameObject.SetActive(false);
            
            if (_assetReferenceDownloader == null)
                ProjectContext.Instance.Container.Inject(this);
        }
        
        [Inject]
        public void Init(IAssetReferenceDownloader assetReferenceDownloader)
        {
            _assetReferenceDownloader = assetReferenceDownloader;
        }

        public void Upload(string spriteName)
        {
            string[] subs = spriteName.Split('/', '\\');
            string source = subs[0];
            string sprite = subs.Length > 1 ? subs[1] : "";
            Upload(source, sprite);
        }

        public void UploadNonAtlas(string spriteName)
        {
            SetInject();
            
            _assetReferenceDownloader.SpawnUnknownById(spriteName, 
                AddImage,
                AddGameObject, 
                spriteAtlas =>
                {
                    AddImage(spriteAtlas, spriteName);
                });
        }

        public void Upload(string source, string spriteName)
        {
            SetInject();
            
            _assetReferenceDownloader.SpawnUnknownById(source, 
                AddImage,
                AddGameObject, 
                spriteAtlas =>
                {
                    AddImage(spriteAtlas, spriteName);
                });
        }

        private void AddImage(Sprite item)
        {
            if(image == null) return;
            image.gameObject.SetActive(true);
            image.sprite = item;
        }

        private void AddImage(SpriteAtlas item, string spriteName)
        {
            AddImage(item.GetSprite(spriteName));
        }

        private void AddGameObject(GameObject newItem)
        {
            if (waitImage)
                Debug.LogWarning("You try instantiate prefab, but wait image!");
            else
                Instantiate(newItem, transform);
        }
    }
}