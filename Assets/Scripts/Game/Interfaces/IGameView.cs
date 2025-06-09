using System;
using MVP.Interfaces;
using UniRx;

namespace Game.Interfaces
{
    public interface IGameView : IView
    {
        public void Initialize();
        public void RestartGame();
        public void AddResources(float multiplier);
        
        public IObservable<Unit> RestartButtonClicked { get; }
    }
}