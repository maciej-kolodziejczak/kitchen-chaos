using Player;
using UnityEngine;

namespace Counter
{
    public class SelectedCounterVisual : MonoBehaviour
    {
        // @todo This is super bad, refactor to Scriptable Object events ASAP 
        [SerializeField] private PlayerInteractions playerInteractions;
        // @todo Figure out a way to reliably select children element with mesh renderer (get in children by type?)
        [SerializeField] private GameObject visualObject;
        private EmptyCounter _emptyCounter;

        private void Awake()
        {
            _emptyCounter = GetComponentInParent<EmptyCounter>();
            playerInteractions.FocusCounter += PlayerInteractionsOnFocusCounter;
        }

        private void PlayerInteractionsOnFocusCounter(EmptyCounter obj)
        {
            if (obj == _emptyCounter)
            {
                Show();
            }
            else
            {
                Hide();
            }
        }

        private void Show()
        {
            visualObject.SetActive(true);
        }

        private void Hide()
        {
            visualObject.SetActive(false);
        }
    
    }
}
