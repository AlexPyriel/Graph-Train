using SceneLoader.Enums;
using SceneLoader.Interfaces;
using UnityEngine;
using Zenject;

namespace Infrastructure
{
    public class BootstrapEntryPoint : MonoBehaviour
    {
        private ISceneLoader _sceneLoader;

        [Inject]
        public void Construct(ISceneLoader sceneLoader)
        {
            _sceneLoader = sceneLoader;
        }
        
        private void Start()
        {
            _sceneLoader.LoadScene(ESceneTypes.TitleScene, false);
        }
    }
}
