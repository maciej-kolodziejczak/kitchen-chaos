using Counter;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    [RequireComponent(typeof(LookAtCamera))]
    public class ProgressBar : MonoBehaviour
    {
        [SerializeField] private Image infill;
        [SerializeField] private CuttingCounter invoker; // @todo introduce interface

        private void Awake()
        {
            invoker.ProgressChanged += OnProgressChanged;
            gameObject.SetActive(false);
        }

        private void OnProgressChanged(float progress)
        {
            gameObject.SetActive(progress is not 0 or >= 1);
            infill.fillAmount = progress;
        }
    }
}