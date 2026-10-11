using UnityEngine;
using Zenject;

public class InteractableObjectsInstaller : MonoInstaller
{
    [SerializeField] private RigidbodyData _keyData;
    [SerializeField] private RigidbodyData _boxData;

    public override void InstallBindings()
    {
        BindKey();
        BindBox();
        BindCloseDoor();
        BindOpenDoor();
    }

    private void BindKey()
    {
        Container.Bind<Key>().FromComponentInHierarchy().AsSingle();

        Container.Bind<RigidbodyData>().WithId(IdNames.Key).FromInstance(_keyData).AsCached();
        Container.Bind<IRigidbodyData>().FromMethod(ctx => ctx.Container.ResolveId<RigidbodyData>(IdNames.Key)).AsCached()
            .WhenInjectedInto<Key>();

        Container.Bind<IInteractable>().WithId(IdNames.Key).FromMethod(ctx => ctx.Container.Resolve<Key>()).AsSingle();
        Container.Bind<IHaveRigidbody>().WithId(IdNames.Key).FromMethod(ctx => ctx.Container.Resolve<Key>()).AsCached();
    }

    private void BindBox()
    {
        Container.Bind<Box>().FromComponentInHierarchy().AsSingle();

        Container.Bind<RigidbodyData>().WithId(IdNames.Box).FromInstance(_boxData).AsCached();
        Container.Bind<IRigidbodyData>().FromMethod(ctx => ctx.Container.ResolveId<RigidbodyData>(IdNames.Box)).AsCached()
            .WhenInjectedInto<Box>();

        Container.Bind<IHaveRigidbody>().WithId(IdNames.Box).FromMethod(ctx => ctx.Container.Resolve<Box>()).AsCached();
    }

    private void BindCloseDoor()
    {
        Container.Bind<CloseDoor>().FromComponentInHierarchy().AsSingle();
        Container.Bind<IDeactivater>().FromMethod(ctx => ctx.Container.Resolve<CloseDoor>()).AsSingle();
    }

    private void BindOpenDoor()
    {
        Container.Bind<OpenDoor>().FromComponentInHierarchy().AsSingle();

        Container.Bind<IActivater>().FromMethod(ctx => ctx.Container.Resolve<OpenDoor>()).AsSingle();
        Container.Bind<IHaveInteractableEvent>().FromMethod(ctx => ctx.Container.Resolve<OpenDoor>()).AsSingle();
        Container.Bind<IInteractable>().WithId(IdNames.OpenDoor).FromMethod(ctx => ctx.Container.Resolve<OpenDoor>()).AsCached();
    }
}