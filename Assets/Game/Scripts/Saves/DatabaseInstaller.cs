using Zenject;

public class DatabaseInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.BindInterfacesTo<Database>().AsSingle();
        Container.BindInterfacesTo<Synchronizer>().AsSingle();
    }
}