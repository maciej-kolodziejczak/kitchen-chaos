using UnityEngine;

namespace Player
{
    public class PlayerKitchenObjectInteraction : MonoBehaviour, IKitchenObjectParent
    {
        [SerializeField] private KitchenObjectSo kitchenObjectSo;
        [SerializeField] private GameObject prefabSpawnGameObject;
        
        private KitchenObject _kitchenObject;
        
        public GameObject GetSpawnOrigin()
        {
            return prefabSpawnGameObject;
        }

        public void AttachKitchenObject(KitchenObject kitchenObject)
        {
            _kitchenObject = kitchenObject.GetComponent<KitchenObject>();
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