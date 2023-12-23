using UnityEngine;

namespace Counter
{
    public class CounterStoveVisual : MonoBehaviour
    {
        [SerializeField] private CounterStove counter;
        [SerializeField] private GameObject particles;
        [SerializeField] private GameObject onIndicator;

        private void Start()
        {
            counter.CookingStarted += OnCookingStarted;
            counter.CookingStopped += OnCookingStopped;
        }

        private void OnCookingStopped()
        {
            particles.SetActive(false);
            onIndicator.SetActive(false);
        }

        private void OnCookingStarted()
        {
            particles.SetActive(true);
            onIndicator.SetActive(true);
        }
    }
}