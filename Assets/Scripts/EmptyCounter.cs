using UnityEngine;

public class EmptyCounter : MonoBehaviour, IInteractable
{
    public void Interact()
    {
        Debug.Log("EmptyCounter interacted");
    }
}
