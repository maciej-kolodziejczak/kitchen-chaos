using KitchenObject;
using UnityEngine;

namespace Counter
{
    public abstract class BaseCounter : MonoBehaviour, IKitchenObjectParent
    {
        [SerializeField] protected Transform kitchenObjectOrigin;

        public IKitchenObjectInteractor Interactor { get; private set; }
        
        private void Awake()
        {
            Interactor = new KitchenObjectInteractor(kitchenObjectOrigin);
        }
        
        public virtual void Interact(IKitchenObjectInteractor interactor) {}
        public virtual void InteractAlt(IKitchenObjectInteractor interactor) {}
    }
}