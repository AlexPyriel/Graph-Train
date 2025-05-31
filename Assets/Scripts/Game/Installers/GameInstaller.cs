using Game.Models;
using Game.Presenters;
using Game.Views;
using Zenject;

namespace Game.Installers
{
    // ReSharper disable once ClassNeverInstantiated.Global
    public class GameInstaller : Installer<GameInstaller>
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
                .BindInterfacesAndSelfTo<GameModel>()
                .FromNew()
                .AsSingle();
        }

        private void BindViews()
        {
            Container
                .BindInterfacesAndSelfTo<GameView>()
                .FromComponentsInHierarchy()
                .AsSingle();
        }

        private void BindPresenters()
        {
            Container
                .BindInterfacesAndSelfTo<GamePresenter>()
                .FromNew()
                .AsSingle();
        }
    }
}