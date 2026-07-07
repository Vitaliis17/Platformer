using UnityEngine;
using Zenject;

public class SaveManager : MonoBehaviour, ISaveManager
{
    [Inject] private ISaveWriter _writer;
    [Inject] private ISaveReader _reader;
    
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