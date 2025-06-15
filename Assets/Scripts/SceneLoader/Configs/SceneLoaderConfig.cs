using System.Collections.Generic;
using AYellowpaper.SerializedCollections;
using SceneLoader.Enums;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace SceneLoader.Configs
{
    [CreateAssetMenu(fileName = "SceneLoaderConfig", menuName = "Configs/New Scene Loader Config", order = 0)]
    public class SceneLoaderConfig : ScriptableObject
    {
        [SerializedDictionary("Scene Type", "Scene Asset Reference")]
        [SerializeField] private SerializedDictionary<ESceneTypes, AssetReference> _scenes;

        [Space, SerializeField] private AssetReference _loadingScene;

        public AssetReference GetSceneReference(ESceneTypes sceneType)
        {
            if (_scenes.TryGetValue(sceneType, out AssetReference sceneReference))
            {
                return sceneReference;
            }

            throw new KeyNotFoundException($"[SceneLoaderConfig] Scene type '{sceneType}' not found in config '{name}'.");
        }

        public AssetReference GetLoadingSceneReference() => _loadingScene;
    }
}