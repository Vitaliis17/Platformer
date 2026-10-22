using System;

[Serializable]
public class SaveData
{
    public int CompleteLevelAmount;
    public int Revision;

    public SaveData(int unlockLevelAmount = 1, int revision = 0)
    {
        CompleteLevelAmount = unlockLevelAmount;
        Revision = revision;
    }
}