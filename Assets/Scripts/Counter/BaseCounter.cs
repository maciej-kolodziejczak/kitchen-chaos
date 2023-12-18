using UnityEngine;

namespace Counter
{
    interface IInteractable
    {
        public void Interact();
    }
    
    public class BaseCounter : MonoBehaviour, IInteractable
    {
        public void Interact()
        {
            Debug.Log("Interacted with counter");
        }
    }
}