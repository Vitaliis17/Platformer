using UnityEngine;
using Zenject;
using R3;

public class RaycastPresenter : MonoBehaviour
{
    [Inject] private ITouchReader _touchReader;
    [Inject] private IHoldReader _holdReader;
    [Inject] private IZoneChecker _zoneChecker;
    [Inject] private IHavePosition _origin;

    [Inject] private IRaycaster<IInteractable> _interactableRaycaster;
    [Inject] private IRaycaster<IInventoryContainer> _inventoryContainerRaycaster;

    [Inject] private ITransferator<IInteractable> _transferator;

    [Inject] private IContainer _container;

    private Vector2 _delta;
    private bool _isPressed;

    private void Start()
    {
        _holdReader.HoldChanged.Subscribe(position => _delta = position);

        _touchReader.PressChanged
            .Where(isPressed => _isPressed == false && isPressed)
            .Select(_ => _inventoryContainerRaycaster.Raycast())
            .Where(container => container != null && container.IsEmpty() == false)
            .Subscribe(container => container.Get())
            .AddTo(this);


        _touchReader.PressChanged
            .Where(isPressing => isPressing)
            .Where(_ => _container.IsEmpty() == false)
            .Subscribe(_ => _transferator.Transfer(_delta, _container.Get()))
            .AddTo(this);

        _touchReader.PressChanged
            .Where(isPressing => isPressing && _isPressed == false)
            .Select(_ => _interactableRaycaster.Raycast())
            .Where(interactable => interactable != null)
            .Where(interactable => _zoneChecker.IsInside(_origin.Position, ((MonoBehaviour)interactable).transform.position))
            .Subscribe(interactable =>
            {
                _isPressed = true;
                _container.Set(interactable);
            }).AddTo(this);

        _touchReader.PressChanged
            .Where(isPressed => _isPressed && isPressed == false)
            .Subscribe(isPressed =>
            {
                SetInventoryContainer();

                _isPressed = isPressed;
                _container.SetEmpty();
            }).AddTo(this);
    }

    private void SetInventoryContainer()
    {
        if (_container.IsEmpty())
            return;

        IInventoryContainer inventoryContainer = _inventoryContainerRaycaster.Raycast();

        if (inventoryContainer == null || inventoryContainer.IsEmpty() == false || _container.IsEmpty())
            return;

        inventoryContainer.Set(_container.Get());
    }
}