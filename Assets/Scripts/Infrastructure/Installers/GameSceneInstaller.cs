using Game.Installers;
using Graph.Installers;

namespace Infrastructure.Installers
{
    public class GameSceneInstaller : ProjectInstaller<GameSceneInstaller>
    {
        public override void InstallBindings()
        {
            Install<GraphInstaller>();
            Install<GameInstaller>();
        }
    }
}
