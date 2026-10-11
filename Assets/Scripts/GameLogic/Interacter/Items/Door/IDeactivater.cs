using R3;

public interface IDeactivater
{
    Observable<Unit> Deactivated { get; }

    void Deactivate(IInteractable interactable);
}