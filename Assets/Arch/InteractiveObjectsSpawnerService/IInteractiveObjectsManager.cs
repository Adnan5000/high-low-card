using System;
using Arch.InteractiveObjectsSpawnerService.Containers;
using UnityEngine;

namespace Arch.InteractiveObjectsSpawnerService
{
    public interface IInteractiveObjectsManager
    {
        void AddContainer(string key, IInteractiveObjectContainer container);
        void RemoveContainer(string key);
        void Instantiate(string prefabId, string containerKey, Action<GameObject> callback = null,
            bool inject = true);
        void Instantiate(string prefabId, IInteractiveObjectContainer container, Action<GameObject> callback = null,
            bool inject = true);
        bool ContainerIsExists(string containerKey);

        public IInteractiveObjectContainer GetContainer(string key);
        
        public Transform GetPoolContainer(string containerKey);
    }
}