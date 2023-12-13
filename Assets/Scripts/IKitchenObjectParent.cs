using UnityEngine;

public interface IKitchenObjectParent
{
        public GameObject GetSpawnOrigin();
        public void AttachKitchenObject(KitchenObject kitchenObject);
        public KitchenObject GetAttachedKitchenObject();
        public void DetachKitchenObject();
        public bool HasAttachedKitchenObject();
}