using UnityEngine;
using R3;

public class OpenDoor : MonoBehaviour, IInteractable, IHaveInteractableEvent, IActivater
{
    private Subject<Unit> _interacted = new();

    public Observable<Unit> Interacted => _interacted;

    public void Interact()
        => _interacted.OnNext(Unit.Default);

    public void Activate()
        => gameObject.SetActive(true);

    private void OnDestroy()
        => _interacted?.Dispose();
}