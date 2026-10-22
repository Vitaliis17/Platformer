using R3;

public interface IhaveDatabaseActivatedEvent
{
    Observable<Unit> DatabaseActivated { get; }
}