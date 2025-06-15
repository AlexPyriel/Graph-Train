using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace SceneLoader.Views
{
    public class LoadingSceneView : MonoBehaviour
    {
        [SerializeField] private Slider _slider;
        [SerializeField] private TextMeshProUGUI _progressLabel;

        private Tween _progressTween;
        private SceneLoadingEvents _sceneLoadingEvents;

        [Inject]
        public void Construct(SceneLoadingEvents sceneLoadingEvents)
        {
            _sceneLoadingEvents = sceneLoadingEvents;
        }

        private void OnEnable()
        {
            ResetProgressBar();
            _sceneLoadingEvents.ReportProgress += UpdateProgressBar;
        }

        private void OnDisable()
        {
            _sceneLoadingEvents.ReportProgress -= UpdateProgressBar;
            _progressTween?.Kill();
        }

        private void ResetProgressBar()
        {
            _slider.value = 0;
            _progressLabel.text = "0%";
        }

        private void UpdateProgressBar(float value)
        {
            _progressTween?.Kill();

            _progressTween = _slider.DOValue(value, 0.3f).SetEase(Ease.OutQuad);

            _progressLabel.text = $"Loading: {Mathf.RoundToInt(value * 100)}%";
        }
    }
}