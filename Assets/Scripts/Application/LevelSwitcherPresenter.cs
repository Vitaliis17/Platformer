using UnityEngine;
using R3;
using System.Collections.Generic;

public class LevelSwitcherPresenter : MonoBehaviour
{
    [SerializeField] private LevelSwitcher _levelSwitcher;

    private SceneLoader _sceneLoader;
    private SceneNamesContainer _container;

    private void Awake()
    {
        _sceneLoader = new();
        _container = new(new Dictionary<int, SceneNames>
        {
            {0, SceneNames.FirstLevel }
        });
    }

    private void Start()
    {
        int minLevel = _levelSwitcher.MinAddressablesLevel;
     
        _levelSwitcher.CurrentLevelChanged.Where(index => index > minLevel)
            .Subscribe(index => _sceneLoader.LoadSceneAsync(_container.GetName(index))).AddTo(this);

        _levelSwitcher.CurrentLevelChanged.Where(index => index <= minLevel).Skip(1)
            .Subscribe(_ => _sceneLoader.LoadMenu()).AddTo(this);
    }
}