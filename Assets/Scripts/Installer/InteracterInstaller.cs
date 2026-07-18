using UnityEngine;
using Zenject;

public class InteracterInstaller : MonoInstaller
{
    [SerializeField] private ScreenData _screenData;

    public override void InstallBindings()
    {
        BindInteracter();
        BindInventoryContainer();
        BindTransferator();
        BindContainer();
    }

    private void BindInteracter()
    {
        Container.Bind<Interacter>().FromComponentInHierarchy().AsSingle();
        Container.Bind<IInteracter>().FromMethod(ctx => ctx.Container.Resolve<Interacter>()).AsSingle();
    }

    private void BindInventoryContainer()
    {
        Container.Bind<InventoryContainer>().FromComponentInHierarchy().AsSingle();
        Container.Bind<IInventoryContainer>().FromMethod(ctx => ctx.Container.Resolve<InventoryContainer>()).AsSingle();
    }

    private void BindTransferator()
    {
        Container.Bind<ScreenData>().FromInstance(_screenData).AsSingle();
        Container.Bind<IPixelPerUnitSender>().FromMethod(ctx => ctx.Container.Resolve<ScreenData>()).AsSingle();

        Container.Bind<ITransferator<ITransferable>>().To<Transferator<ITransferable>>().AsSingle();
    }

    private void BindContainer()
        => Container.Bind<IContainer>().To<Container>().AsSingle();
}