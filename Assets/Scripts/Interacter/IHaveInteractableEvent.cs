using R3;

public interface IHaveInteractableEvent
{
    Observable<Unit> Interacted { get; }
}