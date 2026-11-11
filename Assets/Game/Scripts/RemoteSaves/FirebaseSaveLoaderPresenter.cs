using UnityEngine;
using R3;
using Zenject;
using System.Threading;
using System;
using Cysharp.Threading.Tasks;

public class FirebaseSaveLoaderPresenter : IInitializable, IDisposable
{
    [Inject(Id = ViewNames.SaveClick)] private IHaveOnClickEvent _saveEvent;

    [Inject] private ISaveWriter _localSaveWriter;
    [Inject] private ISaveReaderAsync _remoteSaveReader;

    private CancellationTokenSource _source;
    private IDisposable _disposable;

    public void Initialize()
    {
        _source = new();

        _disposable = _saveEvent.Clicked.Subscribe(_ => Save().Forget());
    }

    public void Dispose()
    {
        _source?.Cancel();
        _source?.Dispose();

        _disposable?.Dispose();
    }

    private async UniTask Save()
    {
        SaveData data = null;

        Debug.Log("SaveButton");
        try
        {
            data = await _remoteSaveReader.Read(_source.Token);
        }
        catch(Exception ex)
        {
            Debug.LogException(ex);
        }

        _localSaveWriter.Write(data);
    }
}