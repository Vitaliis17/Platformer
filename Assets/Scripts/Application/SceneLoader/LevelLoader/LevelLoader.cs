using UnityEngine;
using Zenject;
using R3;

public class LevelLoader : MonoBehaviour, IMenuLoader, ILevelLoader, IHaveLevelLoaderEvent
{
    [Inject] private ISceneLoader _sceneLoader;
    [Inject] private IContainerReceiverByIndex<SceneNames> _container;

    private readonly Subject<int> _levelChanging = new();

    public Observable<int> LevelChanging => _levelChanging;

    public void LoadLevel(int levelNumber)
    {
        SceneNames name = _container.Get(levelNumber);
        
        if (name == SceneNames.None)
            return;

        _levelChanging.OnNext(levelNumber);

        _sceneLoader.LoadSceneAsync(name);
    }

    public void LoadMenu()
        => _sceneLoader.LoadMenu();
}