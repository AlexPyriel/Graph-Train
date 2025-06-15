using System;

namespace SceneLoader
{
    public class SceneLoadingEvents
    {
        public event Action<float> ReportProgress;

        public void InvokeProgress(float progress)
        {
            ReportProgress?.Invoke(progress);
        }
    }
}
