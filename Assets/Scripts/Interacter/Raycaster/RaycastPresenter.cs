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

    private bool _isPressed;

    private void Start()
    {
        SubscribeGettingContainerItem();
        SubscribeSettingContainerItem();

        SubscribeGettingItem();
        SubscribeTransferItem();
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

    private void SubscribeGettingContainerItem()
    {
        _touchReader.PressChanged
            .Where(isPressed => _isPressed == false && isPressed)
            .Select(_ => _inventoryContainerRaycaster.Raycast())
            .Where(container => container != null && container.IsEmpty() == false)
            .Subscribe(container => container.Get())
            .AddTo(this);
    }

    private void SubscribeSettingContainerItem()
    {
        _touchReader.PressChanged
            .Where(isPressed => _isPressed && isPressed == false)
            .Subscribe(isPressed =>
            {
                SetInventoryContainer();

                _isPressed = isPressed;
                _container.SetEmpty();
            }).AddTo(this);
    }

    private void SubscribeGettingItem()
    {
        Observable<bool> pressChanged = _touchReader.PressChanged
            .Where(isPressed => _isPressed == false && isPressed);

        pressChanged
            .Select(_ => _interactableRaycaster.Raycast())
            .Where(interactable => interactable != null)
            .Where(interactable => _zoneChecker.IsInside(_origin.Position, ((MonoBehaviour)interactable).transform.position))
            .Subscribe(interactable => _container.Set(interactable))
            .AddTo(this);
        
        pressChanged
            .Subscribe(isPressed => _isPressed = true)
            .AddTo(this);
    }

    private void SubscribeTransferItem()
    {
        _touchReader.PressChanged
            .Where(isPressing => isPressing)
            .Where(_ => _container.IsEmpty() == false)
            .Select(_ => _holdReader.HoldChanged.CurrentValue)
            .Select(pixelDelta => _transferator.GetDeltaPosition(pixelDelta))
            .Where(delta => IsInside(_container.Get(), delta))
            .Subscribe(delta => _transferator.Transfer(delta, _container.Get()))
            .AddTo(this);
    }

    private bool IsInside(IInteractable interactable, Vector2 delta)
    {
        Vector2 currentPosition = (Vector2)((MonoBehaviour)interactable).transform.position;
        Vector2 nextPosition = delta + currentPosition;

        return _zoneChecker.IsInside(_origin.Position, nextPosition);
    }
}