using UnityEngine;
using Zenject;

public class LevelUnlockerPresenter : MonoBehaviour
{
    [Inject] private ISaveManager _saveManager;
    [Inject] private ILevelUnlocker _levelUnlocker;

    private void Start()
    {
        int currentLevel = _saveManager.ReadCurrentLevel();

        _levelUnlocker.UnlockLevels(currentLevel);
    }
}