using UnityEngine;
using R3;

public class LevelSwitcher : MonoBehaviour
{
    private static LevelSwitcher _instance;

    private ReactiveProperty<int> _currentLevel;

    public int MinAddressablesLevel { get; } = -1;
    public Observable<int> CurrentLevelChanged => _currentLevel;

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        _currentLevel = new(MinAddressablesLevel);
    }

    private void OnDestroy()
        => _currentLevel?.Dispose();

    public void LoadNextLevel()
        => _currentLevel.Value++;

    public void LoadMenu()
        => ResetCurrentLevel();

    private void ResetCurrentLevel()
        => _currentLevel.Value = MinAddressablesLevel;
}
