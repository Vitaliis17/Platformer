using Zenject;

public class FirebaseInstaller : MonoInstaller
{
    public override void InstallBindings()
        => Container.BindInterfacesTo<Authoring>().AsSingle();
}