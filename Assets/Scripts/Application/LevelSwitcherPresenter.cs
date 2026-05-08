using UnityEngine;
using R3;
using Zenject;

public class LevelSwitcherPresenter : MonoBehaviour
{
    [SerializeField] private LevelSwitcher _levelSwitcher;

    [Inject] private ISceneLoader _sceneLoader;
    [Inject] private IContainer<SceneNames> _container;

    private void Start()
    {
        int minLevel = _levelSwitcher.MinAddressablesLevel;

        _levelSwitcher.CurrentLevelChanged.Where(index => index > minLevel)
            .Subscribe(index => _sceneLoader.LoadSceneAsync(_container.Get(index))).AddTo(this);

        _levelSwitcher.CurrentLevelChanged.Where(index => index <= minLevel).Skip(1)
            .Subscribe(_ => _sceneLoader.LoadMenu()).AddTo(this);
    }
}