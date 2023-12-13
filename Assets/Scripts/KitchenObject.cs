using UnityEngine;

public class KitchenObject : MonoBehaviour
{
    [SerializeField] private KitchenObjectSo kitchenObjectSo;
    
    public KitchenObjectSo KitchenObjectSo => kitchenObjectSo;
}
