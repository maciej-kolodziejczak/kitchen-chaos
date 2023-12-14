using KitchenObject;
using UnityEngine;

namespace Player
{
    public class PlayerKitchenObjectInteractor : MonoBehaviour, IKitchenObjectParent

    {
    [SerializeField] private Transform kitchenObjectOrigin;

    private KitchenObject.KitchenObject _kitchenObject;
    public IKitchenObjectInteractor Interactor { get; private set; }

    private void Awake()
    {
        Interactor = new KitchenObjectInteractor(kitchenObjectOrigin);
    }
    }
}