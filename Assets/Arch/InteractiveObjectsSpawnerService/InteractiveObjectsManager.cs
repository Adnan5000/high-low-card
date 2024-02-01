using System;
using System.Collections.Generic;
using Arch.AssetReferences;
using Arch.InteractiveObjectsSpawnerService.Containers;
using UnityEngine;
using Zenject;

namespace Arch.InteractiveObjectsSpawnerService
{
    public class InteractiveObjectsManager: IInteractiveObjectsManager
    {
        private readonly Dictionary<string, IInteractiveObjectContainer> _containers = new Dictionary<string, IInteractiveObjectContainer>();
        
        private IAssetReferenceDownloader _assetReferenceStorage;

        [Inject]
        public void Init(IAssetReferenceDownloader assetReferenceStorage)
        {
            _assetReferenceStorage = assetReferenceStorage;
        }
        
        public void AddContainer(string key, IInteractiveObjectContainer container)
        {
            if (_containers.ContainsKey(key))
            {
                RemoveContainer(key);
            }
            _containers.Add(key, container);
        }
        
        public void RemoveContainer(string key)
        {
             _containers.Remove(key);
        }

        public bool ContainerIsExists(string containerKey)
        {
            return _containers.ContainsKey(containerKey);
        }

        public void Instantiate(string prefabId, string containerKey, Action<GameObject> callback = null, 
            bool inject = true)
        {
            if (!_containers.ContainsKey(containerKey))
            {
                Debug.LogError($"Container {containerKey} don't exist");
                return;
            }
            IInteractiveObjectContainer container = _containers[containerKey];
            Instantiate(prefabId, container, callback, inject);
        }

        public void Instantiate(string prefabId, IInteractiveObjectContainer container,
            Action<GameObject> callback = null, bool inject = true)
        {
            
            if (prefabId == null)
            {
                Debug.Log("Id couldn't be null");
                return;
            }
            if (container == null)
            {
                Debug.LogError("InteractiveObjectsManager   Instantiate  container is null. ");
                return;
            }

            _assetReferenceStorage.SpawnById(prefabId, go =>
            {
                if(go == null)
                    return;
                GameObject item = container.CreateItem(go);
                if(item == null)
                    return;
                item.transform.localScale = Vector3.one;
                if (inject)
                    ProjectContext.Instance.Container.InjectGameObject(item);
                callback?.Invoke(item);
            });
        }

        public IInteractiveObjectContainer GetContainer(string key) => _containers[key]; 
        
        public Transform GetPoolContainer(string containerKey)
        {
            IInteractiveObjectPoolContainer container = _containers[containerKey] as IInteractiveObjectPoolContainer;
            return container.GetTransform;
        }
    }
}