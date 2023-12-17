using System;
using UnityEngine;

namespace Counter
{
    public class StepProgressTracker: MonoBehaviour, IProgressTracker
    {
        private float _currentProgress;
        private float _maxProgress;
        
        public event Action<float> ProgressChanged;
        public event Action ProgressCompleted;
        
        public bool InProgress => _maxProgress > 0 && !IsCompleted;
        public bool IsCompleted =>  _currentProgress >= _maxProgress;
        
        public void StartProgress(float max)
        {
            _maxProgress = max;
            _currentProgress = 0;
            ProgressChanged?.Invoke(_currentProgress);
        }
        
        public void UpdateProgress()
        {
            if (IsCompleted)
            {
                return;
            }
            
            _currentProgress++;
            ProgressChanged?.Invoke(_currentProgress / _maxProgress);

            if (IsCompleted)
            {
                ProgressCompleted?.Invoke();
            }
        }
        
        public void ResetProgress()
        {
            _currentProgress = 0;
            _maxProgress = 0;
            ProgressChanged?.Invoke(0);
        }
    }
}