using Counter;
using UnityEngine;

public class KitchenObject : MonoBehaviour
{
    [SerializeField] private KitchenObjectSo kitchenObjectSo;
    
    public KitchenObjectSo KitchenObjectSo => kitchenObjectSo;

    private IKitchenObjectParent _parent;
    
    public void AttachToParent(IKitchenObjectParent parent)
    {
        _parent = parent;

        var transform1 = transform;
        
        transform1.parent = parent.GetSpawnOrigin().transform;
        transform1.localPosition = Vector3.zero;
    }

    public void DetachFromParent()
    {
        _parent = null;
    }
}
