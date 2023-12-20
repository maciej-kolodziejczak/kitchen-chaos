using Common;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    [RequireComponent(typeof(LookAtCamera))]
    public class ProgressBar : MonoBehaviour
    {
        [SerializeField] private Image infill;
        [SerializeField] private ProgressTracker invoker;

        private void Awake()
        {
            invoker.ProgressChanged += OnProgressChanged;
            gameObject.SetActive(false);
        }

        private void OnProgressChanged(float progress)
        {
            gameObject.SetActive(progress is > 0 and < 1);
            infill.fillAmount = progress;
        }
    }
}