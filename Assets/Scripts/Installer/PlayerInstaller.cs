using UnityEngine;
using Zenject;

public class PlayerInstaller : MonoInstaller
{
    [SerializeField] private MoverData _moverData;
    [SerializeField] private VelocityData _movementVelocityData;
    [SerializeField] private VelocityData _jumpingVelocityData;

    [SerializeField] private Rigidbody2D _rigidbody;

    public override void InstallBindings()
    {
        Vector2 horizontalDirection = Vector2.right;
        Vector2 VerticalDirection = Vector2.up;

        BindMoverData();

        Container.Bind<Rigidbody2D>().FromInstance(_rigidbody);

        Container.Bind<ITransportable>().WithId(IdNames.Horizontal).To<Mover>().AsCached()
            .WithArguments(horizontalDirection);
        Container.Bind<ITransportable>().WithId(IdNames.Vertical).To<Mover>().AsCached()
            .WithArguments(VerticalDirection);

        BindMovementVelocityData();
        BindJumpingVelocityData();

        Container.Bind<IJumpable>().To<Jumper>().AsTransient();

        Container.Bind<Player>().FromComponentInHierarchy().AsSingle();

        Container.Bind<IHavePosition>().FromMethod(ctx => ctx.Container.Resolve<Player>()).AsSingle();
        Container.Bind<IMovable>().FromMethod(ctx => ctx.Container.Resolve<Player>()).AsSingle();
        Container.Bind<IMovableEvents>().FromMethod(ctx => ctx.Container.Resolve<Player>()).AsSingle();
        Container.Bind<IVelocitySetter>().FromMethod(ctx => ctx.Container.Resolve<Player>()).AsSingle();
    }

    private void BindMoverData()
    {
        Container.Bind<MoverData>().FromInstance(_moverData).AsSingle();
        Container.Bind<IHaveSpeed>().FromMethod(ctx => ctx.Container.Resolve<MoverData>()).AsSingle();
    }

    private void BindMovementVelocityData()
    {
        Container.Bind<VelocityData>().WithId(IdNames.Movement).FromInstance(_movementVelocityData).AsCached();
        Container.Bind<IHaveMultiplier>().WithId(IdNames.Movement)
            .FromMethod(ctx => ctx.Container.ResolveId<VelocityData>(IdNames.Movement)).AsCached();
    }

    private void BindJumpingVelocityData()
    {
        Container.Bind<VelocityData>().WithId(IdNames.Jumping).FromInstance(_jumpingVelocityData).AsCached();
        Container.Bind<IHaveMultiplier>().WithId(IdNames.Jumping)
            .FromMethod(ctx => ctx.Container.ResolveId<VelocityData>(IdNames.Jumping)).AsCached();
    }
}