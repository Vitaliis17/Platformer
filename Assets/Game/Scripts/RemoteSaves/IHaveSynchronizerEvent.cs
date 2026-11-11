using R3;

public interface IHaveSynchronizerEvent
{
    Observable<Unit> Synchronized { get; }
}