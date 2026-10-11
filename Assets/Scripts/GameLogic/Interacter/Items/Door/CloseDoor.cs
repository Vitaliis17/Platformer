using UnityEngine;
using R3;
using Zenject;

public class CloseDoor : MonoBehaviour, IDeactivater
{
    [Inject(Id = IdNames.Key)] private IInteractable _key;

    private Subject<Unit> _deactivated = new();

    public Observable<Unit> Deactivated => _deactivated;

    public void Deactivate(IInteractable interactable)
    {
        if (interactable != _key)
            return;

        _deactivated.OnNext(Unit.Default);

        _key.Interact();
        transform.gameObject.SetActive(false);
    }

    private void OnDestroy()
        => _deactivated?.Dispose();
}