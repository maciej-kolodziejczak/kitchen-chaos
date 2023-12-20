using UnityEngine;

public class Holder : MonoBehaviour, IHolder
{
    public Transform HoldPoint => holdPoint;
    public bool IsHolding => AttachedHoldable != null;
    public IHoldable AttachedHoldable { get; private set; }
    
    [SerializeField] private Transform holdPoint;
    
    
    public void Attach(IHoldable holdable)
    {
        AttachedHoldable = holdable;
        holdable.HoldAt(this);
    }

    public void Detach()
    {
        AttachedHoldable = null;
    }
}