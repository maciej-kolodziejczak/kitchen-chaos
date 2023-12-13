using Player;
using UnityEngine;

namespace Counter
{
    public class SelectedCounterVisual : MonoBehaviour
    {
        [SerializeField] private BaseCounter parentCounter;
        [SerializeField] private GameObject[] visualObjects;

        private void Start()
        {
            PlayerInteractions.Instance.FocusCounter += PlayerInteractionsOnFocusCounter;
        }

        private void PlayerInteractionsOnFocusCounter(BaseCounter obj)
        {
            if (obj == parentCounter)
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
            foreach (var visualObject in visualObjects)
            {
                visualObject.SetActive(true);
            }
        }

        private void Hide()
        {
            foreach (var visualObject in visualObjects)
            {
                visualObject.SetActive(false);
            }
        }
    
    }
}
