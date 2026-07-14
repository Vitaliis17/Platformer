using UnityEngine;

[CreateAssetMenu(fileName = nameof(LevelData), menuName = nameof(LevelData))]
public class LevelData : ScriptableObject
{
    public int CurrentLevel { get; private set; }

    public void SetCurrentLevel(int level)
    {
        const int MinLevel = 1;
        
        if (level < MinLevel)
            return;

        CurrentLevel = level;
    }
}