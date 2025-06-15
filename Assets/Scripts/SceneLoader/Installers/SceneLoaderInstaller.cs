using Zenject;

namespace SceneLoader.Installers
{
    // ReSharper disable once ClassNeverInstantiated.Global
    public class SceneLoaderInstaller : Installer<SceneLoaderInstaller>
    {
        public override void InstallBindings()
        {
            Container
                .BindInterfacesAndSelfTo<SceneLoadingEvents>()
                .AsSingle();

            Container
                .BindInterfacesAndSelfTo<SceneLoaderService>()
                .FromNew()
                .AsSingle();
        }
    }
}