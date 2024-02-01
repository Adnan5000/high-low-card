using UnityEngine;

namespace Arch.InteractiveObjectsSpawnerService.Containers
{
    public interface IInteractiveObjectContainer
    {
        GameObject CreateItem(GameObject inst);
        bool ContainerIsExist();
    }
}