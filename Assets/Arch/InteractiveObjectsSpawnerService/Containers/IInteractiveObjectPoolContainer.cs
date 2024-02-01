using UnityEngine;

namespace Arch.InteractiveObjectsSpawnerService.Containers
{
    public interface IInteractiveObjectPoolContainer
    {
        public Transform GetTransform { get; }
    }
}