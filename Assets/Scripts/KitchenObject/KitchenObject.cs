using UnityEngine;

namespace KitchenObject
{
    public class KitchenObject : MonoBehaviour
    {
        [SerializeField] private KitchenObjectSo kitchenObjectSo;
    
        public KitchenObjectSo KitchenObjectSo => kitchenObjectSo;
    
        public void SetToParentOrigin(IKitchenObjectInteractor parent)
        {

            var transform1 = transform;
        
            transform1.parent = parent.GetKitchenObjectOrigin().transform;
            transform1.localPosition = Vector3.zero;
        }
    }
}
