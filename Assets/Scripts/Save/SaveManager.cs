using Zenject;

public class SaveManager : ISaveManager, ISaver
{
    private readonly ISaveWriter _writer;
    private readonly ISaveReader _reader;

    [Inject]
    public SaveManager(ISaveWriter writer, ISaveReader reader)
    {
        _writer = writer;
        _reader = reader;
    }

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