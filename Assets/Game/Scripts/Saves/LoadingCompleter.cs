using Zenject;
using R3;
using UnityEngine;

public class LoadingCompleter : MonoBehaviour, IHaveCompleteEvent
{
    [Inject] private IHaveSynchronizerEvent _synchronizerEvent;
    [Inject(Id = ViewNames.CancelClick)] private IHaveOnClickEvent _cancelEvent;
    [Inject] private IHaveSaveCompareEvent _saveCompleteEvent;

    public Observable<Unit> Completed { get; private set; }

    private void Start()
    {
        Observable<Unit> observable = _saveCompleteEvent.HasFirebaseMoreSaves
            .Where(hasMore => hasMore == false).Select(_ => Unit.Default);
        
        Completed = Observable
            .Merge(_synchronizerEvent.Synchronized, _cancelEvent.Clicked, observable);
    }
}