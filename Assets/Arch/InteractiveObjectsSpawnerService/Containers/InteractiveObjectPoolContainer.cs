using UnityEngine;

namespace Arch.InteractiveObjectsSpawnerService.Containers
{
    public class InteractiveObjectPoolContainer : InteractiveObjectContainer, IInteractiveObjectPoolContainer
    {
        public Transform GetTransform => transform;
    }
}