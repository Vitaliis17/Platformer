using Zenject;
using R3;
using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class Synchronizer : IInitializable, IDisposable, IHaveSynchronizerEvent
{
    [Inject] private ISaverAsync _databaseSaver;
    [Inject] private IHaveSaveEvent _event;

    private CancellationTokenSource _source;

    private IDisposable _disposable;

    private Subject<Unit> _synchronized = new();

    public Observable<Unit> Synchronized => _synchronized;

    public void Initialize()
    {
        _source = new();

        _disposable = _event.SaveChanged.Subscribe(data => Synchronize(data, _source.Token).Forget());
    }

    public void Dispose()
    {
        _source?.Cancel();
        _source?.Dispose();

        _disposable?.Dispose();

        _synchronized?.Dispose();
    }

    private async UniTask Synchronize(SaveData data, CancellationToken token)
    {
        try
        {
            await _databaseSaver.Save(data.CompleteLevelAmount, data.Revision, token);
        }
        catch(Exception ex)
        {
            Debug.Log(ex);
        }

        _synchronized.OnNext(Unit.Default);
    }
}