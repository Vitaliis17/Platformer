using UnityEngine;
using Zenject;
using R3;

public class RaycastPresenter : MonoBehaviour
{
    [Inject] private ITouchReader _touchReader;
    [Inject] private IHoldReader _holdReader;
    [Inject] private IRaycaster<IInteractable> _raycaster;
    [Inject] private IZoneChecker _zoneChecker;
    [Inject] private IHavePosition _origin;

    private Vector2 _delta;
    private IInteractable _interactable;
    private bool _isPressed;

    private void Start()
    {
        _holdReader.HoldChanged.Subscribe(position => _delta = position);

        _touchReader.PressChanged
            .Where(isPressed => _isPressed && isPressed == false)
            .Subscribe(isPressed =>
            {
                _isPressed = isPressed;
                _interactable = null;
            }).AddTo(this);

        _touchReader.PressChanged
            .Where(isPressing => isPressing && _isPressed == false)
            .Select(_ => _raycaster.Raycast())
            .Where(interactable => interactable != null)
            .Where(interactable => _zoneChecker.IsInside(_origin.Position, ((MonoBehaviour)interactable).transform.position))
            .Subscribe(interactable => {
                _isPressed = true;
                _interactable = interactable;
            }).AddTo(this);

        _touchReader.PressChanged
                .Where(isPressing => isPressing)
                .Where(_ => _interactable != null)
                .Select(_ => (MonoBehaviour)_interactable)
                .Subscribe(interactable => Transfer(interactable)).AddTo(this);
    }

    private void Transfer(MonoBehaviour interactable)
    {
        float pixelPerUnit = Camera.main.pixelHeight / (Camera.main.orthographicSize * 2f);
        Vector2 deltaPosition = _delta / pixelPerUnit;

        interactable.transform.position += (Vector3)deltaPosition;
    }
}