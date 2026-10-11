using R3;
using UnityEngine;
using Zenject;

public class LevelDataPresenter : MonoBehaviour
{
    [Inject] private IHaveLevelLoaderEvent _levelEvent;
    [Inject] private ICurrentLevelSetter _currentLevelSetter;

    private void Start()
        => _levelEvent.LevelChanging.Subscribe(levelNumber => _currentLevelSetter.SetCurrentLevel(levelNumber)).AddTo(this);
}