using System;
using Game.Interfaces;
using MVP.Presenters;
using UniRx;
using Zenject;

namespace Game.Presenters
{
    // ReSharper disable once ClassNeverInstantiated.Global
    public class GamePresenter : Presenter<IGameView, IGameModel>, IInitializable, IDisposable
    {
        private readonly CompositeDisposable _disposables;
        public GamePresenter(IGameView view, IGameModel model) : base(view, model)
        {
            _disposables = new CompositeDisposable();
        }

        public void Initialize()
        {
            View.Initialize();
            
            View.RestartButtonClicked
                .Subscribe(_ => RestartGame())
                .AddTo(_disposables);
        }

        private void RestartGame()
        {
            View.RestartGame();
        }

        public void Dispose()
        {
            _disposables?.Dispose();
        }
    }
}