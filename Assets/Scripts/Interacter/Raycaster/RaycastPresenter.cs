using R3;
using System;
using UnityEngine;
using Zenject;

public class RaycastPresenter : MonoBehaviour
{
    [Inject] private ITouchReader _touchReader;
    [Inject] private IHoldReader _holdReader;
    [Inject] private IZoneChecker _zoneChecker;
    [Inject] private IHavePosition _origin;

    [Inject] private IRaycaster<ITransferable> _interactableRaycaster;
    [Inject] private IRaycaster<IInventoryContainer> _inventoryContainerRaycaster;

    [Inject] private ITransferator<ITransferable> _transferator;

    [Inject] private IContainer _container;

    private bool _isPressed;

    private void Start()
    {
        SubscribeGettingContainerItem();
        SubscribeSettingContainerItem();

        SubscribeGettingItem();
        SubscribeTransferItem();
    }

    private bool TrySetInventoryContainer()
    {
        if (_container.IsEmpty())
            return false;

        IInventoryContainer inventoryContainer = _inventoryContainerRaycaster.Raycast();

        if (inventoryContainer == null || inventoryContainer.IsEmpty() == false)
            return false;

        inventoryContainer.Set(_container.Get());
        _container.SetEmpty();

        return true;
    }

    private void SubscribeGettingContainerItem()
    {
        _touchReader.PressChanged
            .Where(isPressed => _isPressed == false && isPressed)
            .Select(_ => _inventoryContainerRaycaster.Raycast())
            .Where(container => container != null && container.IsEmpty() == false)
            .Subscribe(inventoryContainer => inventoryContainer.Get())
            .AddTo(this);
    }

    private void SubscribeSettingContainerItem()
    {
        _touchReader.PressChanged
            .Where(isPressed => _isPressed && isPressed == false)
            .Subscribe(isPressed =>
            {
                if (_container.IsEmpty() == false && TrySetInventoryContainer() == false)
                {
                    ITransferable interactable = _container.Get();
                    interactable.EnablePhysics();
                }

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
            .Where(interactable => _zoneChecker.IsInside(_origin.Position, interactable.Position))
            .Subscribe(interactable =>
            {
                interactable.DisablePhysics();
                _container.Set(interactable);
            })
            .AddTo(this);

        pressChanged
            .Subscribe(_ => _isPressed = true)
            .AddTo(this);
    }

    private void SubscribeTransferItem()
    {
        Observable<Vector2> observable = _touchReader.PressChanged
            .Where(isPressing => isPressing)
            .Where(_ => _container.IsEmpty() == false)
            .Select(_ => _holdReader.HoldChanged.CurrentValue)
            .Select(pixelPosition => _transferator.TranslatePixelPosition(pixelPosition))
            .Where(targetPosition => _zoneChecker.IsInside(_origin.Position, targetPosition));

        observable
            .ThrottleFirst(TimeSpan.FromSeconds(Time.fixedDeltaTime))
            .Subscribe(targetPosition => _transferator.Transfer(targetPosition, _container.Get()))
            .AddTo(this);
    }
}