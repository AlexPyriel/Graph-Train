using TitleScene.Installers;

namespace Infrastructure.Installers
{
    public class TitleSceneInstaller : ProjectInstaller<TitleSceneInstaller>
    {
        public override void InstallBindings()
        {
            Install<TitleScreenInstaller>();
        }
    }
}