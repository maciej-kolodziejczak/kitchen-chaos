using UnityEngine;

namespace KitchenObject
{
    public class KitchenObjectInteractor : IKitchenObjectInteractor
    {
        private global::KitchenObject.KitchenObject _kitchenObject;
        private readonly Transform _kitchenObjectOrigin;
        
        public KitchenObjectInteractor(Transform kitchenObjectOrigin)
        {
            _kitchenObjectOrigin = kitchenObjectOrigin;
        }
        
        public Transform GetKitchenObjectOrigin()
        {
            return _kitchenObjectOrigin;
        }

        public void AttachKitchenObject(global::KitchenObject.KitchenObject kitchenObject)
        {
            _kitchenObject = kitchenObject;
            _kitchenObject.SetToParentOrigin(this);
        }

        public global::KitchenObject.KitchenObject GetAttachedKitchenObject()
        {
            return _kitchenObject;
        }

        public void DetachKitchenObject()
        {
            _kitchenObject = null;
        }

        public bool HasAttachedKitchenObject()
        {
            return _kitchenObject != null;
        }
    }

    public interface IKitchenObjectInteractor
    {
        public Transform GetKitchenObjectOrigin();
        public void AttachKitchenObject(global::KitchenObject.KitchenObject kitchenObject);
        public global::KitchenObject.KitchenObject GetAttachedKitchenObject();
        public void DetachKitchenObject();
        public bool HasAttachedKitchenObject();
    }
}