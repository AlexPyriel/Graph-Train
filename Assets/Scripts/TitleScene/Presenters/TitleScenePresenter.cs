using System;
using MVP.Presenters;
using SceneLoader.Enums;
using SceneLoader.Interfaces;
using TitleScene.Interfaces;
using UniRx;
using Zenject;

namespace TitleScene.Presenters
{
    // ReSharper disable once ClassNeverInstantiated.Global
    public class TitleScenePresenter : Presenter<ITitleSceneView, ITitleSceneModel>, IInitializable, IDisposable
    {
        private readonly CompositeDisposable _disposables;
        private readonly ISceneLoader _sceneLoader;

        public TitleScenePresenter(
            ITitleSceneView view,
            ITitleSceneModel model,
            ISceneLoader sceneLoader
            ) : base(view, model)
        {
            _disposables = new CompositeDisposable();
            _sceneLoader = sceneLoader;
        }

        public void Initialize()
        {
            View.StartButtonClicked
                .Subscribe(_ => LoadGameScene())
                .AddTo(_disposables);
        }

        public void Dispose()
        {
            _disposables?.Dispose();
        }

        private void LoadGameScene()
        {
            _sceneLoader.LoadScene(ESceneTypes.GameScene);
        }
    }
}