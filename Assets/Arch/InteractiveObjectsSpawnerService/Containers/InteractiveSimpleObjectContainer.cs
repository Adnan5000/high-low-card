using UnityEngine;

namespace Arch.InteractiveObjectsSpawnerService.Containers
{
    public class InteractiveSimpleObjectContainer: MonoBehaviour, IInteractiveObjectContainer
    {
        
        
        public GameObject CreateItem(GameObject inst)
        {
            GameObject item = Instantiate(inst, transform, false);
            return item;
        }

        public bool ContainerIsExist()
        {
            return gameObject != null;
        }
    }
}