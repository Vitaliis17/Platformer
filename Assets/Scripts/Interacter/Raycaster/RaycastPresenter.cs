using UnityEngine;
using Zenject;
using R3;

public class RaycastPresenter : MonoBehaviour
{
    [Inject] private ITouchReader _reader;
    [Inject] private IRaycaster<IInteractable> _raycaster;
    [Inject] private IInteracter _interacter;

    private void Start()
        => _reader.PressChanged.Where(isPressing => isPressing)
                .Select(_ => _raycaster.Raycast())
                .Subscribe(interactable => _interacter.Interact(interactable)).AddTo(this);
}