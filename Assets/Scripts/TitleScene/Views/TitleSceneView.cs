using System;
using MVP.Views;
using TitleScene.Interfaces;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace TitleScene.Views
{
    public class TitleSceneView : View, ITitleSceneView
    {
        [SerializeField] private Button _startButton;

        public IObservable<Unit> StartButtonClicked => _startButton.OnClickAsObservable();
    }
}