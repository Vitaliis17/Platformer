using System;

[Serializable]
public class SaveData
{
    public int CompleteLevelAmount;
    public long Revision;

    public SaveData(int unlockLevelAmount = 1, long revision = 0)
    {
        CompleteLevelAmount = unlockLevelAmount;
        Revision = revision;
    }
}