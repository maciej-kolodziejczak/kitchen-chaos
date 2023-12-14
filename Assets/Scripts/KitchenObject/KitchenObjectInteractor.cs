using UnityEngine;
using UnityEngine.Serialization;

namespace KitchenObject
{
    public class KitchenObjectInteractor : MonoBehaviour
    {
        [SerializeField] private Transform kitchenObjectOrigin;

        private KitchenObject _kitchenObject;

        public Transform GetKitchenObjectOrigin()
        {
            return kitchenObjectOrigin;
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
}