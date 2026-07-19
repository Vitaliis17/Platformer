using UnityEngine;
using Zenject;

public class TriggerInstaller : MonoInstaller
{
    [SerializeField] private MapTrigger _ladder;

    [SerializeField] private Trigger _border;
    [SerializeField] private Trigger _groundChecker;

    [SerializeField] private Trigger _movementTrigger;
    [SerializeField] private OnScreenActivater _movementScreenActivater;

    [SerializeField] private Trigger _jumpTrigger;
    [SerializeField] private OnScreenActivater _jumpScreenActivater;

    public override void InstallBindings()
    {
        BindGroundChecker();
        BindBorder();
        BindLadder();
        BindMovementTrigger();
        BindJumpTrigger();
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

    private void BindMovementTrigger()
    {
        Container.Bind<Trigger>().WithId(TriggerNames.MovementStick).FromInstance(_movementTrigger).AsCached();
        Container.Bind<IHaveTriggerEvent>().WithId(TriggerNames.MovementStick)
            .FromMethod(ctx => ctx.Container.ResolveId<Trigger>(TriggerNames.MovementStick)).AsCached();

        Container.Bind<OnScreenActivater>().WithId(TriggerNames.MovementStick).FromInstance(_movementScreenActivater).AsCached();
        Container.Bind<IActivitySetter>().WithId(TriggerNames.MovementStick)
            .FromMethod(ctx => ctx.Container.ResolveId<OnScreenActivater>(TriggerNames.MovementStick)).AsCached();
    }

    private void BindJumpTrigger()
    {
        Container.Bind<Trigger>().WithId(TriggerNames.JumpButton).FromInstance(_jumpTrigger).AsCached();
        Container.Bind<IHaveTriggerEvent>().WithId(TriggerNames.JumpButton)
            .FromMethod(ctx => ctx.Container.ResolveId<Trigger>(TriggerNames.JumpButton)).AsCached();

        Container.Bind<OnScreenActivater>().WithId(TriggerNames.JumpButton).FromInstance(_jumpScreenActivater).AsCached();
        Container.Bind<IActivitySetter>().WithId(TriggerNames.JumpButton)
            .FromMethod(ctx => ctx.Container.ResolveId<OnScreenActivater>(TriggerNames.JumpButton)).AsCached();
    }

}