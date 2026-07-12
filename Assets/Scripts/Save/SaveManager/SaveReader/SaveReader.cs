using System.IO;
using UnityEngine;
using Zenject;

public class SaveReader : ISaveReader
{
    private readonly string _path;

    [Inject]
    public SaveReader(string path)
        => _path = path;

    public bool IsSaveExists()
        => File.Exists(_path);

    public SaveData Read()
    {
        if (IsSaveExists() == false)
            return null;

        string json = File.ReadAllText(_path);

        return JsonUtility.FromJson<SaveData>(json);
    }
}
