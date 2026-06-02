using UnityEngine;
using R3;

public class LevelSwitcher : MonoBehaviour, IMenuLoader, INextLevelLoader
{
    private static LevelSwitcher _instance;

    private ReactiveProperty<int> _currentLevel;

    public int MinAddressablesLevel { get; } = -1;
    public Observable<int> CurrentLevelChanged => _currentLevel;

    private void Awake()
    {
        if (_instance == null)
        {
            _currentLevel = new(MinAddressablesLevel);
            _instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnDestroy()
        => _currentLevel?.Dispose();

    public void LoadNextLevel()
        => _currentLevel.Value++;

    public void LoadMenu()
        => ResetCurrentLevel();

    private void ResetCurrentLevel()
        => _currentLevel.OnNext(MinAddressablesLevel);
}