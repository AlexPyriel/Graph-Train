using TitleScene.Models;
using TitleScene.Presenters;
using TitleScene.Views;
using Zenject;

namespace TitleScene.Installers
{
    // ReSharper disable once ClassNeverInstantiated.Global
    public class TitleScreenInstaller : Installer<TitleScreenInstaller>
    {
        public override void InstallBindings()
        {
            BindModels();
            BindViews();
            BindPresenters();
        }

        private void BindModels()
        {
            Container
                .BindInterfacesAndSelfTo<TitleSceneModel>()
                .FromNew()
                .AsSingle();
        }

        private void BindViews()
        {
            Container
                .BindInterfacesAndSelfTo<TitleSceneView>()
                .FromComponentsInHierarchy()
                .AsSingle();
        }

        private void BindPresenters()
        {
            Container
                .BindInterfacesAndSelfTo<TitleScenePresenter>()
                .FromNew()
                .AsSingle();
        }
    }
}