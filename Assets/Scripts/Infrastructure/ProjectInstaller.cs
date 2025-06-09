using Game.Installers;
using Graph.Installers;
using UnityEngine;
using Zenject;

namespace Infrastructure
{
    public class ProjectInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Install<GraphInstaller>();
            Install<GameInstaller>();
        }
    
        private void Install<T>() where T : Installer<T>
        {
            Installer<T>.Install(Container);
            Debug.Log($"[PROJECT INSTALLER] Install: <b>{typeof(T).Name}</b>");
        }
    }
}
