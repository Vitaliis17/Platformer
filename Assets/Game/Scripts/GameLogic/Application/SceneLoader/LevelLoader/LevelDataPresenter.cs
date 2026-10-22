using R3;
using Zenject;
using System;

public class LevelDataPresenter : IInitializable, IDisposable
{
    [Inject] private IHaveLevelLoaderEvent _levelEvent;
    [Inject] private ICurrentLevelSetter _currentLevelSetter;

    private IDisposable _disposable;

    public void Dispose()
        => _disposable?.Dispose();

    public void Initialize()
        => _disposable = _levelEvent.LevelChanging.Subscribe(levelNumber => _currentLevelSetter.SetCurrentLevel(levelNumber));
}