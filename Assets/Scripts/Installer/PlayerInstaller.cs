using UnityEngine;
using Zenject;

public class PlayerInstaller : MonoInstaller
{
    [SerializeField] private MoverData _moverData;
    [SerializeField] private JumpData _jumpData;

    [SerializeField] private Rigidbody2D _rigidbody;

    public override void InstallBindings()
    {
        Container.Bind<MoverData>().FromScriptableObject(_moverData).AsSingle();
        Container.Bind<Rigidbody2D>().FromInstance(_rigidbody);

        Container.Bind<ITransferable>().WithId(IdNames.Horizontal).To<HorizontalMover>().AsTransient();
        Container.Bind<ITransferable>().WithId(IdNames.Vertical).To<VerticalMover>().AsTransient();

        Container.Bind<JumpData>().FromScriptableObject(_jumpData).AsSingle();

        Container.Bind<IJumpable>().To<Jumper>().AsTransient();

        Container.Bind<Player>().FromComponentInHierarchy().AsSingle();

        Container.Bind<IHavePosition>().FromMethod(ctx => ctx.Container.Resolve<Player>()).AsSingle();
        Container.Bind<IMovable>().FromMethod(ctx => ctx.Container.Resolve<Player>()).AsSingle();
        Container.Bind<IMovableEvents>().FromMethod(ctx => ctx.Container.Resolve<Player>()).AsSingle();
    }
}