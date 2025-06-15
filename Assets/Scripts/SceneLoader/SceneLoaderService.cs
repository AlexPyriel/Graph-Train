using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using SceneLoader.Configs;
using SceneLoader.Enums;
using SceneLoader.Interfaces;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;

namespace SceneLoader
{
    // ReSharper disable once ClassNeverInstantiated.Global
    public class SceneLoaderService : ISceneLoader, IDisposable
    {
        private readonly SceneLoaderConfig _sceneLoaderConfig;
        private readonly SceneLoadingEvents _sceneLoadingEvents;

        private CancellationTokenSource _cancellationTokenSource;
        private AsyncOperationHandle<SceneInstance> _currentSceneHandle;

        public SceneLoaderService(SceneLoadingEvents sceneLoadingEvents)
        {
            _sceneLoaderConfig = Resources.Load<SceneLoaderConfig>(nameof(SceneLoaderConfig));
            _cancellationTokenSource = new CancellationTokenSource();
            _sceneLoadingEvents = sceneLoadingEvents;
        }

        public async void LoadScene(ESceneTypes sceneType, bool showLoadingScreen = true)
        {
            ResetCancellationTokenSource();

            if (showLoadingScreen)
                await LoadSceneWithLoadingScreen(sceneType);
            else
                await LoadSceneDirect(sceneType);
        }

        private async UniTask LoadSceneDirect(ESceneTypes sceneType)
        {
            AssetReference targetSceneReference = _sceneLoaderConfig.GetSceneReference(sceneType);

            AsyncOperationHandle<SceneInstance> targetSceneHandle = targetSceneReference.LoadSceneAsync(activateOnLoad: false);
            await targetSceneHandle.ToUniTask(cancellationToken: _cancellationTokenSource.Token);

            if (!targetSceneHandle.IsValid() || targetSceneHandle.Status != AsyncOperationStatus.Succeeded)
            {
                throw new InvalidOperationException($"Failed to load scene: {sceneType}");
            }

            ReleaseSceneIfValid(_currentSceneHandle);

            _currentSceneHandle = targetSceneHandle;
            SceneInstance targetSceneInstance = targetSceneHandle.Result;

            await ActivateAndSetActive(targetSceneInstance);
        }

        private async UniTask LoadSceneWithLoadingScreen(ESceneTypes sceneType)
        {
            Scene currentScene = SceneManager.GetActiveScene();

            AssetReference loadingSceneReference = _sceneLoaderConfig.GetLoadingSceneReference();
            AsyncOperationHandle<SceneInstance> loadingSceneHandle = loadingSceneReference.LoadSceneAsync(LoadSceneMode.Additive);

            await loadingSceneHandle.ToUniTask(cancellationToken: _cancellationTokenSource.Token);

            if (!loadingSceneHandle.IsValid() || loadingSceneHandle.Status != AsyncOperationStatus.Succeeded)
            {
                throw new InvalidOperationException($"Failed to load scene: Loading Scene");
            }

            SceneInstance loadingSceneInstance = loadingSceneHandle.Result;
            Scene loadingScene = loadingSceneInstance.Scene;
            SceneManager.SetActiveScene(loadingScene);

            await SceneManager.UnloadSceneAsync(currentScene);

            ReleaseSceneIfValid(_currentSceneHandle);

            AssetReference targetSceneReference = _sceneLoaderConfig.GetSceneReference(sceneType);
            AsyncOperationHandle<SceneInstance> targetSceneHandle = targetSceneReference.LoadSceneAsync(LoadSceneMode.Additive, false);

            await WaitForLoadingWithProgress(targetSceneHandle);

            if (!targetSceneHandle.IsValid() || targetSceneHandle.Status != AsyncOperationStatus.Succeeded)
            {
                throw new InvalidOperationException($"Failed to load scene: {sceneType}");
            }

            _currentSceneHandle = targetSceneHandle;
            SceneInstance targetSceneInstance = targetSceneHandle.Result;

            await ActivateAndSetActive(targetSceneInstance);
            await SceneManager.UnloadSceneAsync(loadingScene);

            ReleaseSceneIfValid(loadingSceneHandle);

            _sceneLoadingEvents.InvokeProgress(1f);
        }

        public void Dispose()
        {
            ResetCancellationTokenSource();
        }

        private async UniTask ActivateAndSetActive(SceneInstance sceneInstance)
        {
            await sceneInstance.ActivateAsync();
            SceneManager.SetActiveScene(sceneInstance.Scene);
        }

        private async UniTask WaitForLoadingWithProgress(AsyncOperationHandle<SceneInstance> handle)
        {
            while (!handle.IsDone)
            {
                _sceneLoadingEvents.InvokeProgress(handle.PercentComplete);
                await UniTask.Yield(PlayerLoopTiming.Update, _cancellationTokenSource.Token);
            }
        }

        private void ReleaseSceneIfValid(AsyncOperationHandle<SceneInstance> handle)
        {
            if (handle.IsValid())
            {
                Addressables.Release(handle);
            }
        }

        private void ResetCancellationTokenSource()
        {
            _cancellationTokenSource?.Cancel();
            _cancellationTokenSource?.Dispose();
            _cancellationTokenSource = new CancellationTokenSource();
        }
    }
}