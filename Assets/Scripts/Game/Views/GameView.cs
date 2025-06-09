using System;
using Game.Interfaces;
using MVP.Views;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Views
{
    public class GameView : View, IGameView
    {
        [Header("UI")]
        [SerializeField] private Button _restartButton;
        [SerializeField] private TextMeshProUGUI _totalResourcesLabel;
        [Space]
        [SerializeField] private TrainSpawner _trainSpawner;
        
        private float _totalResources;
        private string _totalResourcesLabelTemplate;

        public IObservable<Unit> RestartButtonClicked => _restartButton.OnClickAsObservable();

        public void Initialize()
        {
            UpdateResourcesLabel(0);

            _trainSpawner.SpawnTrains(this);
        }

        public void RestartGame()
        {
            UpdateResourcesLabel(0);

            _trainSpawner.DespawnTrains();
            _trainSpawner.SpawnTrains(this);
        }

        public void AddResources(float multiplier)
        {
            _totalResources += 1f * multiplier;
            UpdateResourcesLabel(_totalResources);
        }

        private void UpdateResourcesLabel(float amount)
        {
            _totalResourcesLabelTemplate ??= _totalResourcesLabel.text;

            _totalResourcesLabel.text = string.Format(_totalResourcesLabelTemplate, amount);
        }
    }
}