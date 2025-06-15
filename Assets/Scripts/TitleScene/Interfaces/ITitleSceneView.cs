using System;
using MVP.Interfaces;
using UniRx;

namespace TitleScene.Interfaces
{
    public interface ITitleSceneView : IView
    {
        public IObservable<Unit> StartButtonClicked { get; }
    }
}