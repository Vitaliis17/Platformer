using UnityEngine;
using R3;
using Zenject;

[RequireComponent(typeof(Collider2D))]
public class OpenDoorPresenter : MonoBehaviour
{
    [Inject(Id = IdNames.OpenDoor)] private IInteractable _interactable;
    [Inject] private IHaveInteractableEvent _interactableEvent;

    [Inject] private IPauseSwitcher _pauseSwitcher;
    
    [SerializeField] private Transform _panel;

    private void Start()
    {
        _interactableEvent.Interacted.Subscribe(_ =>
        {
            _pauseSwitcher.Pause();
            _panel.gameObject.SetActive(true);
        }
        ).AddTo(this);
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.transform.TryGetComponent(out IInteracter interacter))
            interacter.Interact(_interactable);
    }
}