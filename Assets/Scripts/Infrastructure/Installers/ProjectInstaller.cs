using UnityEngine;
using Zenject;

namespace Infrastructure.Installers
{
    public abstract class ProjectInstaller<TInstaller> : MonoInstaller
    {
        protected void Install<T>() where T : Installer<T>
        {
            Installer<T>.Install(Container);
            Debug.Log($"[{typeof(TInstaller).Name}] Install: <b>{typeof(T).Name}</b>");
        }
    }
}