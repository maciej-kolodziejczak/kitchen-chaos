using UnityEngine;

public interface IKitchenObjectParent
{
        public Transform GetKitchenObjectOrigin();
        public void AttachKitchenObject(KitchenObject.KitchenObject kitchenObject);
        public KitchenObject.KitchenObject GetAttachedKitchenObject();
        public void DetachKitchenObject();
        public bool HasAttachedKitchenObject();
}