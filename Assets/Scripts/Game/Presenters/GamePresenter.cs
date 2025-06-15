using System;
using Game.Interfaces;
using MVP.Presenters;
using SceneLoader.Enums;
using SceneLoader.Interfaces;
using UniRx;
using Zenject;

namespace Game.Presenters
{
    // ReSharper disable once ClassNeverInstantiated.Global
    public class GamePresenter : Presenter<IGameView, IGameModel>, IInitializable, IDisposable
    {
        private readonly CompositeDisposable _disposables;
        private readonly ISceneLoader _sceneLoader;

        public GamePresenter(
            IGameView view,
            IGameModel model,
            ISceneLoader sceneLoader
            ) : base(view, model)
        {
            _disposables = new CompositeDisposable();
            _sceneLoader = sceneLoader;
        }

        public void Initialize()
        {
            View.Initialize();
            
            View.RestartButtonClicked
                .Subscribe(_ => RestartGame())
                .AddTo(_disposables);

            View.TitleButtonClicked
                .Subscribe(_ => LoadTitleScene())
                .AddTo(_disposables);
        }

        private void RestartGame()
        {
            View.RestartGame();
        }

        private void LoadTitleScene()
        {
            _sceneLoader.LoadScene(ESceneTypes.TitleScene);
        }

        public void Dispose()
        {
            _disposables?.Dispose();
        }
    }
}