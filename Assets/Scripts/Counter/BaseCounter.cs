using System;
using KitchenObject;
using UnityEngine;

namespace Counter
{
    [RequireComponent(typeof(KitchenObjectInteractor))]
    public abstract class BaseCounter : MonoBehaviour
    {
        protected KitchenObjectInteractor Interactor;
        
        public void Awake()
        {
            Interactor = GetComponent<KitchenObjectInteractor>();
        }

        public virtual void Interact(KitchenObjectInteractor interactor) {}
        public virtual void InteractAlt(KitchenObjectInteractor interactor) {}
    }
}