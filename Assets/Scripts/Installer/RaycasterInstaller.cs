using UnityEngine;
using Zenject;

public class RaycasterInstaller : MonoInstaller
{
    [SerializeField] private LayerMask _inventoryContainerLayer;
    [SerializeField] private LayerMask _interactableLayer;

    public override void InstallBindings()
    {
        Container.Bind<LayerMask>().FromInstance(_interactableLayer).WhenInjectedInto<IRaycaster<IInteractable>>();
        Container.Bind<IRaycaster<IInteractable>>().To<Raycaster<IInteractable>>().AsSingle();

        Container.Bind<LayerMask>().FromInstance(_inventoryContainerLayer).WhenInjectedInto<IRaycaster<IInventoryContainer>>();
        Container.Bind<IRaycaster<IInventoryContainer>>().To<Raycaster<IInventoryContainer>>().AsSingle();
    }
}