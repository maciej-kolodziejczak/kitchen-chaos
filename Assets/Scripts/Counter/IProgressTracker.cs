using System;

namespace Counter
{
    public interface IProgressTracker
    {
        public event Action<float> ProgressChanged;
        public event Action ProgressCompleted;
        public bool InProgress { get; }
        public void StartProgress(float max);
        public void ResetProgress();
    }
}