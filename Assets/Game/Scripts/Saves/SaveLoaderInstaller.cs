using Zenject;

public class SaveLoaderInstaller : MonoInstaller
{
    public override void InstallBindings()
        => Container.BindInterfacesTo<SaveLoader>().AsSingle();
}