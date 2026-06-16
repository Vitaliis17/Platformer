using UnityEngine;
using Zenject;

public class LevelSwitcher : MonoBehaviour, IMenuLoader, INextLevelLoader, ICurrentLevelReloader
{
    [Inject] private ISceneLoader _sceneLoader;
    [Inject] private IContainerReceiverByIndex<SceneNames> _container;

    private static int _currentLevel;
    private readonly int _minLevelValue = 0;

    public void LoadNextLevel()
    {
        _currentLevel++;

        if (_currentLevel > _minLevelValue)
            _sceneLoader.LoadSceneAsync(_container.Get(_currentLevel));
    }

    public void LoadMenu()
        => _sceneLoader.LoadMenu();

    public void ReloadCurrentLevel()
        => _sceneLoader.LoadSceneAsync(_container.Get(_currentLevel));

    public void ResetCurrentLevel()
        => _currentLevel = _minLevelValue;
}