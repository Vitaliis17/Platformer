using R3;

public interface IDeactivaterByParameter
{
    Observable<Unit> Deactivated { get; }

    void Deactivate(IInteractable interactable);
}