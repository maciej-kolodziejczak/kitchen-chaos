using UnityEngine;

namespace KitchenObject
{
    public class KitchenObjectInteractor : IKitchenObjectInteractor
    {
        private KitchenObject _kitchenObject;
        private readonly Transform _kitchenObjectOrigin;
        
        public KitchenObjectInteractor(Transform kitchenObjectOrigin)
        {
            _kitchenObjectOrigin = kitchenObjectOrigin;
        }
        
        public Transform GetKitchenObjectOrigin()
        {
            return _kitchenObjectOrigin;
        }

        public void AttachKitchenObject(KitchenObject kitchenObject)
        {
            _kitchenObject = kitchenObject;
            _kitchenObject.SetToParentOrigin(this);
        }

        public KitchenObject GetAttachedKitchenObject()
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
        public void AttachKitchenObject(KitchenObject kitchenObject);
        public KitchenObject GetAttachedKitchenObject();
        public void DetachKitchenObject();
        public bool HasAttachedKitchenObject();
    }
}