using UnityEngine;
using Zenject;

public class TriggerInstaller : MonoInstaller
{
    [SerializeField] private MapTrigger _ladder;

    [SerializeField] private Trigger _border;
    [SerializeField] private Trigger _groundChecker;

    public override void InstallBindings()
    {
        BindGroundChecker();
        BindBorder();
        BindLadder();
    }

    private void BindGroundChecker()
    {
        Container.Bind<Trigger>().WithId(TriggerNames.GroundChecker).FromInstance(_groundChecker).AsCached();
        Container.Bind<IHaveTriggerEvent>().WithId(TriggerNames.GroundChecker)
            .FromMethod(ctx => ctx.Container.ResolveId<Trigger>(TriggerNames.GroundChecker)).AsCached();
    }

    private void BindBorder()
    {
        Container.Bind<Trigger>().WithId(TriggerNames.Border).FromInstance(_border).AsCached();
        Container.Bind<IHaveTriggerEvent>().WithId(TriggerNames.Border)
            .FromMethod(ctx => ctx.Container.ResolveId<Trigger>(TriggerNames.Border)).AsCached();
    }

    private void BindLadder()
    {
        Container.Bind<MapTrigger>().FromInstance(_ladder).AsSingle();
        Container.Bind<IHaveTriggerEvent>().WithId(TriggerNames.Ladder).FromMethod(ctx => ctx.Container.Resolve<MapTrigger>()).AsCached();
    }
}