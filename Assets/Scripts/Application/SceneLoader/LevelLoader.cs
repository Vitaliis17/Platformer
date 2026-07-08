using UnityEngine;
using Zenject;

public class LevelLoader : MonoBehaviour, IMenuLoader, ILevelLoader
{
    [Inject] private ISceneLoader _sceneLoader;
    [Inject] private IContainerReceiverByIndex<SceneNames> _container;

    public void LoadLevel(int levelNumber)
    {
        SceneNames name = _container.Get(levelNumber);

        if (name == SceneNames.None)
            return;

        _sceneLoader.LoadSceneAsync(name);
    }

    public void LoadMenu()
        => _sceneLoader.LoadMenu();
}