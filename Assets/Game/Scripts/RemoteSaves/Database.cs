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
        Debug.Log(_reference);

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
            
            revision = Convert.ToInt32(snapshot.Value);
        }
        catch (Exception exception)
        {
            Debug.LogError(exception);
        }

        return revision;
    }

    public async UniTask Save(int levelNumber, long revision, CancellationToken token)
    {
        Debug.Log(_reference);
        if (_reference == null)
            return;

        try
        {
            await _reference
                .RunTransaction(data => MakeTransaction(data, levelNumber, revision))
                .AsUniTask()
                .AttachExternalCancellation(token);

            Debug.Log("save");
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
            long revision = Convert.ToInt64(snapshot.Child(FirebaseChildrenNames.Revision).Value);

            saveData = new SaveData(currentLevel, revision);
        }
        catch(Exception ex)
        {
            Debug.LogError(ex);
        }

        return saveData;
    }

    private TransactionResult MakeTransaction(MutableData data, int number, long revision)
    {
        long firebaseRevision = data.HasChild(FirebaseChildrenNames.Revision) ? 
            Convert.ToInt64(data.Child(FirebaseChildrenNames.Revision).Value) : 0;

        Debug.Log("A");
        if (firebaseRevision > revision)
        {
            return TransactionResult.Abort();
        }
        Debug.Log("B");

        data.Value = new Dictionary<string, object>()
        {
            [FirebaseChildrenNames.CurrentLevel] = number,
            [FirebaseChildrenNames.Revision] = revision
        };
        Debug.Log("C");

        return TransactionResult.Success(data);
    }
}
