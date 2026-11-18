using R3;

public interface IDeactivaterWithEvent
{
    Observable<Unit> Deactivated { get; }

    void Deactivate();
}