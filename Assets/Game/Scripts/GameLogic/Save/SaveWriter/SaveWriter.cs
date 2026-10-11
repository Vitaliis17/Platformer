using UnityEngine;
using System.IO;
using Zenject;

public class SaveWriter : ISaveWriter
{
    private readonly string _path;

    [Inject]
    public SaveWriter(string path)
        => _path = path;

    public void Save(SaveData data)
    {
        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText(_path, json);
    }
}