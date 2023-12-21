using Common;
using UnityEngine;

public class Plate : MonoBehaviour, IHoldable, IDestroyable
{
    public void HoldAt(IHolder holder)
    {
        var transform1 = transform;
        
        transform.SetParent(holder.HoldPoint);
        transform1.localPosition = Vector3.zero;
        transform1.localRotation = Quaternion.identity;
    }
    
    public void Release()
    {
        transform.SetParent(null);
    }
    
    public void Destroy()
    {
        Destroy(gameObject);
    }
}