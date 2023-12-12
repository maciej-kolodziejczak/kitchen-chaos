using UnityEngine;

namespace Counter
{
    public class EmptyCounter : MonoBehaviour, IInteractable
    {
        public void Interact()
        {
            Debug.Log("EmptyCounter interacted");
        }
    }
}
