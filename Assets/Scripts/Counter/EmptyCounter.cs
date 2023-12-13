using UnityEngine;

namespace Counter
{
    public class EmptyCounter : MonoBehaviour, IInteractable
    {
        [SerializeField] private KitchenObjectSo kitchenObject;
        [SerializeField] private GameObject prefabSpawnPoint;
        
        public void Interact()
        {
            var tomato = Instantiate(kitchenObject.prefab, prefabSpawnPoint.transform);
            kitchenObject.prefab.transform.localPosition = Vector3.zero;
            Debug.Log("EmptyCounter interacted");
        }
    }
}
