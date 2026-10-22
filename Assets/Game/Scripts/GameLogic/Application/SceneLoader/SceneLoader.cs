using System.Threading;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;
using Zenject;
using Cysharp.Threading.Tasks;

public class SceneLoader : ISceneLoader, ISceneDisposer
{
    private readonly CancellationTokenSource _source;

    private AsyncOperationHandle<SceneInstance> _handle;

    [Inject]
    public SceneLoader(CancellationTokenSource source)
        => _source = source;

    public UniTask LoadSceneAsync(SceneNames name, LoadSceneMode loadMode = LoadSceneMode.Single)
        => LoadSceneAsync(name, loadMode, _source.Token);

    public void LoadMenu()
    {
        const int BaseSceneIndex = 0;

        SceneManager.LoadSceneAsync(BaseSceneIndex);
    }

    public void SceneDispose()
    {
        Debug.Log("Dispose");
        Addressables.UnloadSceneAsync(_handle);
    }

    public void Dispose()
    {
        if (_handle.IsValid())
            _handle.Release();

        _source?.Cancel();
        _source?.Dispose();
    }

    private async UniTask LoadSceneAsync(SceneNames sceneName, LoadSceneMode loadMode, CancellationToken token)
    {
        if (sceneName == SceneNames.None || token.IsCancellationRequested)
            return;

        _handle = Addressables.LoadSceneAsync(sceneName.ToString(), loadMode);

        await _handle.Task;

        if (_handle.Status == AsyncOperationStatus.Succeeded)
            SceneManager.SetActiveScene(_handle.Result.Scene);
    }
}