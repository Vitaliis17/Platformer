using UnityEngine;
using Zenject;

public class InteracterInstaller : MonoInstaller
{
    [SerializeField] private ScreenData _screenData;

    public override void InstallBindings()
    {
        Container.Bind<Interacter>().FromComponentInHierarchy().AsSingle();
        Container.Bind<IInteracter>().FromMethod(ctx => ctx.Container.Resolve<Interacter>()).AsSingle();

        Container.Bind<IContainer>().To<Container>().AsSingle();

        Container.Bind<InventoryContainer>().FromComponentInHierarchy().AsSingle();
        Container.Bind<IInventoryContainer>().FromMethod(ctx => ctx.Container.Resolve<InventoryContainer>()).AsSingle();

        Container.Bind<ScreenData>().FromScriptableObject(_screenData).AsSingle();
        Container.Bind<Transferator>().FromComponentInHierarchy().AsSingle();
        Container.Bind<ITransferator<IInteractable>>().FromMethod(ctx => ctx.Container.Resolve<Transferator>()).AsSingle();
    }
}