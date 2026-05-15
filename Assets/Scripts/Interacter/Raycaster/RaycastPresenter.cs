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

    [Inject] private ITransferator<IInteractable> _transferator;
    [Inject] private IContainer<IInteractable> _container;

    private Vector2 _delta;
    private bool _isPressed;

    private void Start()
    {
        _holdReader.HoldChanged.Subscribe(position => _delta = position);

        _touchReader.PressChanged
            .Where(isPressed => _isPressed && isPressed == false)
            .Subscribe(isPressed =>
            {
                _isPressed = isPressed;
                _container.SetEmpty();
            }).AddTo(this);

        _touchReader.PressChanged
            .Where(isPressing => isPressing && _isPressed == false)
            .Select(_ => _raycaster.Raycast())
            .Where(interactable => interactable != null)
            .Where(interactable => _zoneChecker.IsInside(_origin.Position, ((MonoBehaviour)interactable).transform.position))
            .Subscribe(interactable => {
                _isPressed = true;
                _container.Set(interactable);
            }).AddTo(this);

        _touchReader.PressChanged
                .Where(isPressing => isPressing)
                .Where(_ => _container.IsEmpty() == false)
                .Subscribe(_ => _transferator.Transfer(_delta, _container.Get())).AddTo(this);
    }
}