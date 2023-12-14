using UnityEngine;

namespace Player
{
    public class PlayerKitchenObjectInteractor : MonoBehaviour
    {
        [SerializeField] private Transform kitchenObjectOrigin;
        
        private KitchenObject.KitchenObject _kitchenObject;
        public KitchenObjectInteractor Interactor { get; private set; }

        private void Awake()
        {
            Interactor = new KitchenObjectInteractor(kitchenObjectOrigin);
        }
    }
}