using KitchenObject;
using UnityEngine;

namespace Counter
{
    public class KitchenObjectSpawner : MonoBehaviour
    {
        public KitchenObject.KitchenObject SpawnKitchenObject(KitchenObjectSo kitchenObjectSo, Transform origin)
        {
            var newKitchenObject = Instantiate(kitchenObjectSo.prefab, origin);
            return newKitchenObject.GetComponent<KitchenObject.KitchenObject>();
        }
    }
}