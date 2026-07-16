using UnityEngine;

[CreateAssetMenu(fileName = nameof(LevelData), menuName = nameof(LevelData))]
public class LevelData : ScriptableObject, ICurrentLevelSetter
{
    public int CurrentLevel { get; private set; }

    private void OnEnable()
        => CurrentLevel = 1;

    public void SetCurrentLevel(int level)
    {
        const int MinLevel = 1;
        
        if (level < MinLevel)
            return;

        CurrentLevel = level;
    }
}