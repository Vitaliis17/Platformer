using Zenject;

public class InteractableObjectsInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<Key>().FromComponentInHierarchy().AsSingle();
        Container.Bind<IInteractable>().WithId(IdNames.Key).FromMethod(ctx => ctx.Container.Resolve<Key>()).AsCached();

        Container.Bind<CloseDoor>().FromComponentInHierarchy().AsSingle();
        Container.Bind<IDeactivater>().FromMethod(ctx => ctx.Container.Resolve<CloseDoor>()).AsSingle();

        Container.Bind<OpenDoor>().FromComponentInHierarchy().AsSingle();

        Container.Bind<IActivater>().FromMethod(ctx => ctx.Container.Resolve<OpenDoor>()).AsSingle();
        Container.Bind<IHaveInteractableEvent>().FromMethod(ctx => ctx.Container.Resolve<OpenDoor>()).AsSingle();
        Container.Bind<IInteractable>().WithId(IdNames.OpenDoor).FromMethod(ctx => ctx.Container.Resolve<OpenDoor>()).AsCached();
    }
}