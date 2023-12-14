using KitchenObject;
using UnityEngine;

public interface IKitchenObjectParent
{
        public IKitchenObjectInteractor Interactor { get; }
}