using System;

[Serializable]
public class SaveData
{
    public int CompleteLevelAmount;

    public SaveData(int unlockLevelAmount = 1)
        => CompleteLevelAmount = unlockLevelAmount;
}