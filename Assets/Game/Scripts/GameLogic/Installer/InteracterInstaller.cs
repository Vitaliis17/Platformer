using UnityEngine;
using Zenject;

public class InteracterInstaller : MonoInstaller
{
    [SerializeField] private TransparencyData _containerTransparencyData;

    [SerializeField] private TransparencySwitcher _container;
    [SerializeField] private TransparencySwitcher _border;

    [SerializeField] private InventoryContainer _inventoryContainer;

    public override void InstallBindings()
    {
        BindInteracter();
        BindInventoryContainer();
        BindTransparencySwitcher();
        BindTransferator();
        BindContainer();
    }

    private void BindInteracter()
    {
        Container.Bind<Interacter>().FromComponentInHierarchy().AsSingle();
        Container.Bind<IInteracter>().FromMethod(ctx => ctx.Container.Resolve<Interacter>()).AsSingle();
    }
    
    private void BindInventoryContainer()
        => Container.BindInterfacesTo<InventoryContainer>().FromInstance(_inventoryContainer).AsSingle();

    private void BindTransparencySwitcher()
    {
        Container.Bind<TransparencyData>().FromInstance(_containerTransparencyData).AsSingle();
        Container.Bind<ITransparencyData>().FromMethod(ctx => ctx.Container.Resolve<TransparencyData>()).AsSingle();

        Container.Bind<ITransparencySwitcher>().WithId(TransparencySwitcherId.Container).FromInstance(_container).AsCached();
        Container.Bind<ITransparencySwitcher>().WithId(TransparencySwitcherId.Border).FromInstance(_border).AsCached();
    }

    private void BindTransferator()
        => Container.Bind<ITransferator<ITransferable>>().To<Transferator<ITransferable>>().AsSingle();

    private void BindContainer()
        => Container.Bind<IContainer>().To<Container>().AsSingle();
}