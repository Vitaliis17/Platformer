using UnityEngine;
using Zenject;
using R3;

public class RaycastPresenter : MonoBehaviour
{
    [Inject] private ITouchReader _touchReader;
    [Inject] private IHoldReader _holdReader;
    [Inject] private IRaycaster<IInteractable> _raycaster;
    [Inject] private IInteracter _interacter;
    [Inject] private IZoneChecker _zoneChecker;
    [Inject] private IHavePosition _origin;

    private Vector2 _holdPosition;

    private void Start()
    {
        _holdReader.HoldChanged.Subscribe(position => _holdPosition = position);

        _touchReader.PressChanged
                .Where(isPressing => isPressing)
                .Select(_ => _raycaster.Raycast())
                .Where(interactable => interactable != null)
                .Select(interactable => (MonoBehaviour)interactable)
                .Where(interactable => _zoneChecker.IsInside(_origin.Position, interactable.transform.position))
                .Subscribe(interactable => Transfer(interactable)).AddTo(this);
    }

    private void Transfer(MonoBehaviour interactable)
    {
        float pixelPerUnit = Camera.main.pixelHeight / (Camera.main.orthographicSize * 2f);
        Debug.Log(Camera.main.pixelHeight);
        Debug.Log(Camera.main.orthographicSize);
        Debug.Log(_holdPosition);
        Vector2 delta = _holdPosition / pixelPerUnit;
        interactable.transform.position += (Vector3)delta;
        Debug.Log(interactable.transform.position);
        Debug.Log(delta);
        Debug.Log((Vector3)delta);
    }
}