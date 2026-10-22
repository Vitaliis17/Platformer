using UnityEngine;
using System.IO;
using Zenject;
using System;
using R3;

public class SaveWriter : ISaveWriter, IHaveSaveEvent, IDisposable
{
    private readonly string _path;

    private readonly Subject<SaveData> _saveChanged = new();

    public Observable<SaveData> SaveChanged => _saveChanged;

    [Inject]
    public SaveWriter(string path)
    {
        _path = path;

        _saveChanged = new();
    }

    public void Write(SaveData data)
    {
        if (data == null)
            return;

        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText(_path, json);

        _saveChanged.OnNext(data);
    }

    public void Dispose()
        => _saveChanged?.Dispose();
}