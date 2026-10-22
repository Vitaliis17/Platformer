using Zenject;

public class LevelUnlockerPresenter : IInitializable
{
    [Inject] private ISaveManager _saveManager;
    [Inject] private ILevelUnlocker _levelUnlocker;

    public void Initialize()
    {
        int currentLevel = _saveManager.ReadCurrentLevel();

        _levelUnlocker.UnlockLevels(currentLevel);
    }
}