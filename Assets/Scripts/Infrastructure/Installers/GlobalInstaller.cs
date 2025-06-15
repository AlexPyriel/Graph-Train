using SceneLoader.Installers;

namespace Infrastructure.Installers
{
    public class GlobalInstaller : ProjectInstaller<GlobalInstaller>
    {
        public override void InstallBindings()
        {
            Install<SceneLoaderInstaller>();
        }
    }
}