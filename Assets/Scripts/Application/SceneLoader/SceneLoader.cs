using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.SceneManagement;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;
using System;
using System.Threading;
using Zenject;

public class SceneLoader : IDisposable, ISceneLoader
{
    private readonly CancellationTokenSource _source;
    
    private AsyncOperationHandle<SceneInstance> _handle;

    [Inject]
    public SceneLoader(CancellationTokenSource source)
        => _source = source;

    public void LoadSceneAsync(SceneNames name)
        => LoadSceneAsync(name, _source.Token);

    public void LoadMenu()
    {
        const int BaseSceneIndex = 0;

        SceneManager.LoadSceneAsync(BaseSceneIndex);
    }

    public void Dispose()
    {
        if (_handle.IsValid())
            _handle.Release();

        _source?.Cancel();
        _source?.Dispose();
    }

    private async void LoadSceneAsync(SceneNames sceneName, CancellationToken token)
    {
        if (sceneName == SceneNames.None || token.IsCancellationRequested)
            return;

        if (_handle.IsValid())
            Addressables.UnloadSceneAsync(_handle);

        _handle = Addressables.LoadSceneAsync(sceneName.ToString());

        await _handle.Task;

        if (_handle.Status == AsyncOperationStatus.Succeeded)
            SceneManager.SetActiveScene(_handle.Result.Scene);
    }
}