using UnityEngine;
using Zenject;

public class GameplayReaderInstaller : MonoInstaller
{
    [SerializeField] private ZoneCheckerData _zoneCheckerData;

    public override void InstallBindings()
    {
        BindGameplayAction();
        BindZoneChecker();
    }

    private void BindGameplayAction()
    {
        Container.Bind<GameplayAction>().FromComponentInHierarchy().AsSingle();

        Container.Bind<IMovementReader>().FromMethod(ctx => ctx.Container.Resolve<GameplayAction>()).AsSingle();
        Container.Bind<ITouchReader>().FromMethod(ctx => ctx.Container.Resolve<GameplayAction>()).AsSingle();
        Container.Bind<IJumpReader>().FromMethod(ctx => ctx.Container.Resolve<GameplayAction>()).AsSingle();
        Container.Bind<IHoldReader>().FromMethod(ctx => ctx.Container.Resolve<GameplayAction>()).AsSingle();
    }

    private void BindZoneChecker()
    {
        Container.Bind<ZoneCheckerData>().FromInstance(_zoneCheckerData).AsSingle();
        Container.Bind<IHaveRadius>().FromMethod(ctx => ctx.Container.Resolve<ZoneCheckerData>()).AsSingle();

        Container.Bind<IZoneChecker>().To<ZoneChecker>().AsSingle();
    }
}