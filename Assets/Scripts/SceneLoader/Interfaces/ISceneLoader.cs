using SceneLoader.Enums;

namespace SceneLoader.Interfaces
{
    public interface ISceneLoader
    {
        public void LoadScene(ESceneTypes sceneType, bool showLoadingScreen = true);
    }
}