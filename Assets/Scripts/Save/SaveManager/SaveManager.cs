using UnityEngine;
using Zenject;

public class SaveManager : MonoBehaviour, ISaveManager, ISaver
{
    [Inject] private ISaveWriter _writer;
    [Inject] private ISaveReader _reader;

    public void Save(int levelNumber)
    {
        if (levelNumber <= ReadCurrentLevel())
            return;

        SaveData data = new(levelNumber);

        _writer.Save(data);
    }
    
    public void ResetLevels()
    {
        SaveData newData = new();

        _writer.Save(newData);
    }

    public int ReadCurrentLevel()
    {
        if (_reader.IsSaveExists() == false)
            ResetLevels();

        SaveData save = _reader.Read();

        return save.CompleteLevelAmount;
    }
}