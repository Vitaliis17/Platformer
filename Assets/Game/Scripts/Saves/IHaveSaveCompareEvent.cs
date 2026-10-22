using R3;

public interface IHaveSaveCompareEvent
{
    Observable<bool> HasFirebaseMoreSaves { get; }
}