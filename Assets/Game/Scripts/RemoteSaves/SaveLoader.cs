using Zenject;
using UnityEngine.SceneManagement;
using UnityEngine;
using R3;
using System;
using Cysharp.Threading.Tasks;

public class SaveLoader : IInitializable, IDisposable
{
    [Inject] private ISceneLoader _sceneLoader;
    [Inject] private ISceneDisposer _sceneDisposer;

    private IDisposable _disposable;

    public void Initialize()
        => Execute();

    private async void Execute()
    {
        await _sceneLoader.LoadSceneAsync(SceneNames.Loading, LoadSceneMode.Additive);

        SubscribeDisposer();
    }

    public void Dispose()
        => _disposable?.Dispose();

    private void SubscribeDisposer()
    {
        Scene scene = SceneManager.GetSceneByName(SceneNames.Loading.ToString());

        if (scene.IsValid() == false)
            return;

        foreach (GameObject gameObject in scene.GetRootGameObjects())
        {
            if (gameObject.TryGetComponent(out IHaveCompleteEvent completeEvent))
            {
                _disposable = completeEvent.Completed.Subscribe(_ => _sceneDisposer.SceneDispose());
            }
        }
    }
}