using Common;
using UnityEngine;

namespace Counter
{
    [RequireComponent(typeof(Holder))]
    public abstract class CounterBase : MonoBehaviour, IInteractable<IHolder>
    {
        [SerializeField] private GameObject focusGameObject;
    
        protected Holder Holder;

        protected virtual void Awake()
        {
            Holder = GetComponent<Holder>();
        }
    
        public abstract void Interact(IHolder invoker);
    
        
        public void Focus()
        {
            focusGameObject.SetActive(true);
        }
        
        public void Blur()
        {
            focusGameObject.SetActive(false);
        }
    }
}