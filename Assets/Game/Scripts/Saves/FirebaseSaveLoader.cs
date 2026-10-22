using Zenject;
using Cysharp.Threading.Tasks;
using System.Threading;
using System;
using R3;

public class FirebaseSaveLoader : IInitializable, IHaveSaveCompareEvent, IDisposable
{
    [Inject] private IRevisionReader _localRevisionReader;
    [Inject] private IRevisionReaderAsync _firebaseRevisionReader;
    [Inject] private IhaveDatabaseActivatedEvent _activatedEvent;

    private Subject<bool> _hasFirebaseMoreSaves = new();

    private CancellationTokenSource _source;
    private IDisposable _disposable;

    public Observable<bool> HasFirebaseMoreSaves => _hasFirebaseMoreSaves;

    public void Initialize()
    {
        _source = new();

        _disposable = _activatedEvent.DatabaseActivated.Subscribe(_ => CompareRevisions(_source.Token).Forget());
    }

    public void Dispose()
    {
        _source?.Cancel();
        _source?.Dispose();

        _hasFirebaseMoreSaves?.Dispose();
        _disposable?.Dispose();
    }

    private async UniTask CompareRevisions(CancellationToken token)
    {
        int localRevision = _localRevisionReader.ReadRevision();
        
        int firebaseRevision = await _firebaseRevisionReader.ReadRevision().AttachExternalCancellation(token);
        UnityEngine.Debug.Log("firebase " + firebaseRevision);
        UnityEngine.Debug.Log("local " + localRevision);
        _hasFirebaseMoreSaves.OnNext(firebaseRevision > localRevision);
    }
}