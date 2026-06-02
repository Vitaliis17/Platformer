using UnityEngine;
using R3;
using Zenject;

[RequireComponent(typeof(Collider2D))]
public class OpenDoorPresenter : MonoBehaviour
{
    [Inject(Id = IdNames.OpenDoor)] private IInteractable _interactable;
    [Inject] private IHaveInteractableEvent _interactableEvent;

    [Inject] private IMenuLoader _menuLoader;

    private void Start()
        => _interactableEvent.Interacted.Subscribe(_ => _menuLoader.LoadMenu()).AddTo(this);

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.transform.TryGetComponent(out IInteracter interacter))
            interacter.Interact(_interactable);
    }
}