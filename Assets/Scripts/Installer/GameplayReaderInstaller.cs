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
        => Container.BindInterfacesTo<GameplayAction>().FromComponentInHierarchy().AsSingle();

    private void BindZoneChecker()
    {
        Container.Bind<ZoneCheckerData>().FromInstance(_zoneCheckerData).AsSingle();
        Container.Bind<IHaveRadius>().FromMethod(ctx => ctx.Container.Resolve<ZoneCheckerData>()).AsSingle();

        Container.Bind<IZoneChecker>().To<ZoneChecker>().AsSingle();
    }
}