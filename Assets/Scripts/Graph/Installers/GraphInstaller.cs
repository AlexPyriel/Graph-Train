using Graph.Models;
using Graph.Presenters;
using Graph.Views;
using Zenject;

namespace Graph.Installers
{
    // ReSharper disable once ClassNeverInstantiated.Global
    public class GraphInstaller : Installer<GraphInstaller>
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
                .BindInterfacesAndSelfTo<GraphModel>()
                .FromNew()
                .AsSingle();
        }

        private void BindViews()
        {
            Container
                .BindInterfacesAndSelfTo<GraphView>()
                .FromComponentsInHierarchy()
                .AsSingle();
        }

        private void BindPresenters()
        {
            Container
                .BindInterfacesAndSelfTo<GraphPresenter>()
                .FromNew()
                .AsSingle();
        }
    }
}