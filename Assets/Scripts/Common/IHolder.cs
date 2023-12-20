using UnityEngine;

namespace Common
{
    public interface IHolder
    {
        public Transform HoldPoint { get; }
    
        public bool IsHolding { get; }
        public IHoldable AttachedHoldable { get; }
    
        public void Attach(IHoldable holdable);
        public void Detach();
    }
}