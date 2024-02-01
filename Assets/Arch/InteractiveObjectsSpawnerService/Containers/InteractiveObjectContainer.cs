using UnityEngine;
using Zenject;

namespace Arch.InteractiveObjectsSpawnerService.Containers
{
    public class InteractiveObjectContainer : MonoBehaviour, IInteractiveObjectContainer
    {
        [SerializeField] private string containerId;

        private IInteractiveObjectsManager _interactive;
        private bool _isDestroyed;

        public void Start()
        {
            ProjectContext.Instance.Container.Inject(this);
        }

        [Inject]
        public void Init(IInteractiveObjectsManager interactive)
        {
            _interactive = interactive;
            _interactive.AddContainer(containerId, this);
        }

        public GameObject CreateItem(GameObject inst)
        {
            if(_isDestroyed)
                return null;
            GameObject item = Instantiate(inst, transform, false);
            return item;
        }

        public bool ContainerIsExist()
        {
            //if(this.TryGetComponent(out GameObject obj))
            return this != null;
        }

        public void OnDestroy()
        { 
            _interactive?.RemoveContainer(containerId);
            _isDestroyed = true;
        }
    }
}