using System;
using UnityEngine;
using UnityEngine.UI;

namespace Counter
{
    public class ProgressBarUI : MonoBehaviour
    {
        [SerializeField] private Image barImage;
        [SerializeField] private CuttingCounter cuttingCounter;

        private void Start()
        {
            barImage.fillAmount = 0;
            cuttingCounter.ProgressChanged += OnProgressChanged;

            gameObject.SetActive(false);
        }
        
        private void OnProgressChanged(float fill)
        {
            if (fill is 0 or >= 1)
            {
                gameObject.SetActive(false);
                return;
            }
            
            barImage.fillAmount = fill;
            gameObject.SetActive(true);
        }
    }
}