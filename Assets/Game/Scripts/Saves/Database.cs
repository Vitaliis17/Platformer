using Firebase.Database;
using Cysharp.Threading.Tasks;
using System.Threading;
using System.Collections.Generic;
using System;
using Firebase;
using UnityEngine;
using R3;

public class Database : IReferenceSetter, IRevisionReaderAsync, ISaverAsync, ISaveReaderAsync, IhaveDatabaseActivatedEvent
{
    private DatabaseReference _reference;

    private Subject<Unit> _databaseActivated = new();

    public Observable<Unit> DatabaseActivated => _databaseActivated;

    public void SetReference(string userId)
    {
        Debug.Log("1 SetReference");

        _reference = FirebaseDatabase.DefaultInstance
            .GetReference(FirebaseChildrenNames.Users)
            .Child(userId);

        _databaseActivated.OnNext(Unit.Default);
    }

    public async UniTask<int> ReadRevision()
    {
        int revision = -1;

        if (_reference == null)
            return revision;

        try
        {
            DataSnapshot snapshot = await _reference
                .Child(FirebaseChildrenNames.Revision)
                .GetValueAsync();
            UnityEngine.Debug.Log(_reference);
            UnityEngine.Debug.Log(_reference.Child(FirebaseChildrenNames.Revision));
            UnityEngine.Debug.Log(snapshot.Value);
            revision = Convert.ToInt32(snapshot.Value);
        }
        catch (Exception exception)
        {
            Debug.LogError(exception);
        }

        return revision;
    }

    public async UniTask Save(int levelNumber, int revision, CancellationToken token)
    {
        if (_reference == null)
            return;

        try
        {
            await _reference
                .RunTransaction(data => MakeTransaction(data, levelNumber, revision))
                .AsUniTask()
                .AttachExternalCancellation(token);
        }
        catch (FirebaseException exception)
        {
            Debug.LogError(exception);
        }
    }


    public async UniTask<SaveData> Read(CancellationToken token)
    {
        SaveData saveData = null;

        if (_reference == null)
            return saveData;

        try
        {
            DataSnapshot snapshot = await _reference
                .GetValueAsync()
                .AsUniTask()
                .AttachExternalCancellation(token);

            if (snapshot.Exists == false)
                return saveData;

            int currentLevel = Convert.ToInt32(snapshot.Child(FirebaseChildrenNames.CurrentLevel).Value);
            int revision = Convert.ToInt32(snapshot.Child(FirebaseChildrenNames.Revision).Value);

            saveData = new SaveData(currentLevel, revision);
        }
        catch(Exception ex)
        {
            Debug.LogError(ex);
        }

        return saveData;
    }

    private TransactionResult MakeTransaction(MutableData data, int number, int revision)
    {
        int firebaseRevision = data.HasChild(FirebaseChildrenNames.Revision) ? 
            Convert.ToInt32(data.Child(FirebaseChildrenNames.Revision).Value) : 0;

        if (firebaseRevision > revision)
            return TransactionResult.Abort();

        data.Value = new Dictionary<string, object>()
        {
            [FirebaseChildrenNames.CurrentLevel] = number,
            [FirebaseChildrenNames.Revision] = revision
        };

        return TransactionResult.Success(data);
    }
}
