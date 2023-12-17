using UnityEngine;

namespace KitchenObject
{
    public class KitchenObject : MonoBehaviour
    {
        [SerializeField] private KitchenObjectSo kitchenObjectSo;
    
        public KitchenObjectSo KitchenObjectSo => kitchenObjectSo;

        private void Awake()
        {
           Instantiate(kitchenObjectSo.visualPrefab, transform);
        }

        public void SetToParentOrigin(KitchenObjectInteractor parent)
        {

            var transform1 = transform;
            
            transform1.parent = parent.GetKitchenObjectOrigin().transform;
            transform1.localPosition = Vector3.zero;
        }

        public void DestroySelf()
        {
            Destroy(gameObject);
        }
    }
}
