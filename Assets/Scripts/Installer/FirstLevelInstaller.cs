using UnityEngine;
using Zenject;

public class FirstLevelInstaller : MonoInstaller
{
    [SerializeField] private MoverData _moverData;
    [SerializeField] private JumpData _jumpData;
    [SerializeField] private ZoneCheckerData _zoneCheckerData;
    [SerializeField] private ScreenData _screenData;

    public override void InstallBindings()
    {
        BindGameplayReader();
        BindInteracter();
        BindPlayer();
        BindRaycaster();
    }

    private void BindGameplayReader()
    {
        Container.Bind<GameplayAction>().FromComponentInHierarchy().AsSingle();

        Container.Bind<IMovementReader>().FromMethod(ctx => ctx.Container.Resolve<GameplayAction>()).AsSingle();
        Container.Bind<ITouchReader>().FromMethod(ctx => ctx.Container.Resolve<GameplayAction>()).AsSingle();
        Container.Bind<IJumpReader>().FromMethod(ctx => ctx.Container.Resolve<GameplayAction>()).AsSingle();
        Container.Bind<IHoldReader>().FromMethod(ctx => ctx.Container.Resolve<GameplayAction>()).AsSingle();

        Container.Bind<ZoneCheckerData>().FromScriptableObject(_zoneCheckerData).AsSingle();
        Container.Bind<IZoneChecker>().To<ZoneChecker>().AsTransient();
    }

    private void BindInteracter()
    {
        Container.Bind<Interacter>().FromComponentInHierarchy().AsSingle();
        Container.Bind<IInteracter>().FromMethod(ctx => ctx.Container.Resolve<Interacter>()).AsSingle();

        Container.Bind<IContainer<IInteractable>>().To<Container>().AsSingle();

        Container.Bind<ScreenData>().FromScriptableObject(_screenData).AsSingle();
        Container.Bind<Transferator>().FromComponentInHierarchy().AsSingle();
        Container.Bind<ITransferator<IInteractable>>().FromMethod(ctx => ctx.Container.Resolve<Transferator>()).AsSingle();
    }

    private void BindRaycaster()
    {
        Container.Bind<Raycaster>().FromComponentInHierarchy().AsSingle();
        Container.Bind<IRaycaster<IInteractable>>().FromMethod(ctx => ctx.Container.Resolve<Raycaster>()).AsSingle();
    }

    private void BindPlayer()
    {
        Container.Bind<MoverData>().FromScriptableObject(_moverData).AsSingle();

        Container.Bind<IMoveable>().WithId(IdNames.Horizontal).To<HorizontalMover>().AsTransient();
        Container.Bind<IMoveable>().WithId(IdNames.Vertical).To<VerticalMover>().AsTransient();

        Container.Bind<JumpData>().FromScriptableObject(_jumpData).AsSingle();
        Container.Bind<IJumpable>().To<Jumper>().AsTransient();

        Container.Bind<Player>().FromComponentInHierarchy().AsSingle();
        Container.Bind<IHavePosition>().FromMethod(ctx => ctx.Container.Resolve<Player>()).AsSingle();
    }
}
