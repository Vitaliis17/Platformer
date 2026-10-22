using R3;
using System;
using UnityEngine;
using Zenject;

public class RaycastPresenter : IInitializable, IDisposable
{
    [Inject] private ITouchReader _touchReader;
    [Inject] private IHoldReader _holdReader;
    [Inject] private IZoneChecker _zoneChecker;
    [Inject] private IHavePosition _origin;

    [Inject] private IRaycaster<ITransferable> _interactableRaycaster;
    [Inject] private IRaycaster<IInventoryContainer> _inventoryContainerRaycaster;

    [Inject] private ITransferator<ITransferable> _transferator;

    [Inject] private IContainer _container;
    [Inject] private IInventoryContainer _inventoryContainer;

    private bool _isPressed;

    private DisposableBag _bag;

    public void Initialize()
    {
        SubscribeGettingContainerItem();
        SubscribeSettingContainerItem();

        SubscribeGettingItem();
        SubscribeTransferItem();
    }

    public void Dispose()
    {
        _container.Dispose();
        _bag.Dispose();
    }

    private bool IsInventoryContanerItem(ITransferable transferable)
    {
        IInventoryContainer container = _inventoryContainerRaycaster.Raycast();

        if (container == null)
            return false;

        return container.IsEqual(transferable);
    }

    private bool TrySetInventoryContainer()
    {
        if (_container.IsEmpty())
            return false;

        IInventoryContainer inventoryContainer = _inventoryContainerRaycaster.Raycast();

        if (inventoryContainer == null || inventoryContainer.IsEmpty() == false)
            return false;

        ITransferable interactable = _container.Get();
        interactable.TurnOnTrigger();

        inventoryContainer.Set(interactable);
        _container.SetEmpty();

        return true;
    }

    private void SubscribeGettingContainerItem()
    {
        _bag.Add(_touchReader.PressChanged
            .Where(isPressed => _isPressed == false && isPressed)
            .Where(_ => _container == null || _container.IsEmpty())
            .Select(_ => _inventoryContainerRaycaster.Raycast())
            .Where(container => container != null && container.IsEmpty() == false)
            .Subscribe(inventoryContainer => {
                ITransferable interactable = inventoryContainer.Get();
                interactable.TurnOffTrigger();
                }));
    }

    private void SubscribeSettingContainerItem()
    {
        _bag.Add(_touchReader.PressChanged
            .Where(isPressed => _isPressed && isPressed == false)
            .Subscribe(isPressed =>
            {
                if (_container.IsEmpty() == false && TrySetInventoryContainer() == false)
                {
                    ITransferable interactable = _container.Get();
                    interactable.EnablePhysics();
                    _container.SetEmpty();
                }

                _isPressed = isPressed;
            }));
    }

    private void SubscribeGettingItem()
    {
        Observable<bool> pressChanged = _touchReader.PressChanged
            .Where(isPressed => _isPressed == false && isPressed);

        _bag.Add(pressChanged
            .Where(_ => _inventoryContainer.IsEmpty())
            .Select(_ => _interactableRaycaster.Raycast())
            .Where(interactable => interactable != null)
            .Where(interactable => IsInventoryContanerItem(interactable) == false)
            .Where(interactable => _zoneChecker.IsInside(_origin.Position, interactable.Position))
            .Subscribe(interactable =>
            {
                interactable.DisablePhysics();
                _container.Set(interactable);
            }));

        _bag.Add(pressChanged
            .Subscribe(_ => _isPressed = true));
    }

    private void SubscribeTransferItem()
    {
        Observable<Vector2> observable = _touchReader.PressChanged
            .Where(isPressing => isPressing)
            .Where(_ => _container.IsEmpty() == false)
            .Select(_ => _holdReader.HoldChanged.CurrentValue)
            .Select(pixelPosition => _transferator.TranslatePixelPosition(pixelPosition))
            .Where(targetPosition => _zoneChecker.IsInside(_origin.Position, targetPosition));

        _bag.Add(observable
            .ThrottleFirst(TimeSpan.FromSeconds(Time.fixedDeltaTime))
            .Subscribe(targetPosition => _transferator.Transfer(targetPosition, _container.Get())));
    }
}