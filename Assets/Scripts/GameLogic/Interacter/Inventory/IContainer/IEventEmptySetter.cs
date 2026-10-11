using R3;

public interface IEventEmptySetter
{
    Observable<Unit> EmptySetted { get; }
    
    void SetEmpty();
}