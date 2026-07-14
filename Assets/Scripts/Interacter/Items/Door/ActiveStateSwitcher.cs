using UnityEngine;
using R3;
using Zenject;

public class ActiveStateSwitcher : MonoBehaviour
{
    [Inject] private IDeactivater _deactivater;
    [Inject] private IActivater _activater;

    private void Start()
        => _deactivater.Deactivated.Subscribe(_ => _activater.Activate()).AddTo(this);

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform.TryGetComponent(out IInteractable interactable))
            _deactivater.Deactivate(interactable);
    }
}