using UnityEngine;
using Zenject;

public class RaycasterInstaller : MonoInstaller
{
    [SerializeField] private LayerMask _inventoryContainerLayer;
    [SerializeField] private LayerMask _interactableLayer;

    public override void InstallBindings()
    {
        BindInteractableRaycaster();
        BindInventoryContainer();
    }

    private void BindInteractableRaycaster()
    {
        Container.Bind<Raycaster<IInteractable>>().AsSingle().WithArguments(_interactableLayer);
        Container.Bind<IRaycaster<IInteractable>>().FromMethod(ctx => ctx.Container.Resolve<Raycaster<IInteractable>>()).AsSingle();
    }

    private void BindInventoryContainer()
    {
        Container.Bind<Raycaster<IInventoryContainer>>().AsSingle().WithArguments(_inventoryContainerLayer);
        Container.Bind<IRaycaster<IInventoryContainer>>().FromMethod(ctx => ctx.Container.Resolve<Raycaster<IInventoryContainer>>()).AsSingle();
    }
}