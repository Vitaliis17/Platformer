using R3;

public interface IHaveCompleteEvent
{
    Observable<Unit> Completed { get; }
}