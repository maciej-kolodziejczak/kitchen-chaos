using System;
using UnityEngine;

namespace Counter
{
    public class ProgressTracker : MonoBehaviour
    {
        private float _progress = 0;
        private float _maxProgress;
        
        public bool HasStarted => _maxProgress > 0;
        public bool IsInProgress => _progress < _maxProgress;
        public bool IsFinished => _progress >= _maxProgress;
        
        public event Action<float> ProgressChanged;
        
        public void StartProgress(float maxProgress)
        {
            _maxProgress = maxProgress;
            _progress = 0;
            
            ProgressChanged?.Invoke(0);
        }

        public void Progress(float by)
        {
            _progress += by;
            
            ProgressChanged?.Invoke(_progress / _maxProgress);
        }

        public void ResetProgress()
        {
            _progress = 0;
            _maxProgress = 0;
            
            ProgressChanged?.Invoke(0);
        }
    }
}