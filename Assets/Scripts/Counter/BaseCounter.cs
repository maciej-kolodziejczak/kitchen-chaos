using Player;
using UnityEngine;

namespace Counter
{
    public abstract class BaseCounter : MonoBehaviour, IInteractable<PlayerInteractions>, IKitchenObjectParent
    {
        [SerializeField] protected GameObject prefabSpawnGameObject;

        private KitchenObject _kitchenObject;
        
        public virtual void Interact(PlayerInteractions playerInteractions) {}
        
        public GameObject GetSpawnOrigin()
        {
            return prefabSpawnGameObject;
        }

        public void AttachKitchenObject(KitchenObject kitchenObject)
        {
            _kitchenObject = kitchenObject;
            _kitchenObject.AttachToParent(this);
        }

        public KitchenObject GetAttachedKitchenObject()
        {
            return _kitchenObject;
        }

        public void DetachKitchenObject()
        {
            _kitchenObject.DetachFromParent();
            _kitchenObject = null;
        }

        public bool HasAttachedKitchenObject()
        {
            return _kitchenObject != null;
        }
    }
}