using UnityEngine;
using Zenject;

public class PlayerInstaller : MonoInstaller
{
    [SerializeField] private MoverData _moverData;
    [SerializeField] private JumpData _jumpData;

    [SerializeField] private Rigidbody2D _rigidbody;

    public override void InstallBindings()
    {
        Vector2 horizontalDirection = Vector2.right;
        Vector2 VerticalDirection = Vector2.up;

        Container.Bind<MoverData>().FromInstance(_moverData).AsSingle();
        Container.Bind<Rigidbody2D>().FromInstance(_rigidbody);

        Container.Bind<ITransportable>().WithId(IdNames.Horizontal).To<Mover>().AsCached()
            .WithArguments(horizontalDirection);
        Container.Bind<ITransportable>().WithId(IdNames.Vertical).To<Mover>().AsCached()
            .WithArguments(VerticalDirection);

        Container.Bind<JumpData>().FromInstance(_jumpData).AsSingle();

        Container.Bind<IJumpable>().To<Jumper>().AsTransient();

        Container.Bind<Player>().FromComponentInHierarchy().AsSingle();

        Container.Bind<IHavePosition>().FromMethod(ctx => ctx.Container.Resolve<Player>()).AsSingle();
        Container.Bind<IMovable>().FromMethod(ctx => ctx.Container.Resolve<Player>()).AsSingle();
        Container.Bind<IMovableEvents>().FromMethod(ctx => ctx.Container.Resolve<Player>()).AsSingle();
    }
}