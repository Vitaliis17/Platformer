using Zenject;

public class SaveManager : IRevisionReader, ISaveManager, ISaver, ISaveReseter
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

        int revision = ReadRevision() + 1;

        SaveData data = new(levelNumber, revision);

        _writer.Write(data);
    }
    
    public void ResetLevels()
    {
        SaveData newData = new();

        _writer.Write(newData);
    }

    public int ReadCurrentLevel()
    {
        if (_reader.IsSaveExists() == false)
            ResetLevels();

        SaveData save = _reader.Read();

        return save.CompleteLevelAmount;
    }

    public int ReadRevision()
    {
        if (_reader.IsSaveExists() == false)
            ResetLevels();

        SaveData save = _reader.Read();

        return save.Revision;
    }
}