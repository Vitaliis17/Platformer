using R3;

public interface IEventContainerSetter
{
    Observable<Unit> Setted { get; }

    void Set(ITransferable setter);
}