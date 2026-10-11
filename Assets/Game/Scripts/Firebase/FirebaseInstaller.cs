using Zenject;
using UnityEngine;

public class FirebaseInstaller : MonoInstaller
{
    [SerializeField] private LoginView _loginView;
    [SerializeField] private RegisterView _registerView;

    public override void InstallBindings()
    {
        Container.BindInterfacesTo<Authoring>().AsSingle();

        Container.BindInterfacesTo<LoginPresenter>().AsSingle();
        Container.BindInterfacesTo<RegisterPresenter>().AsSingle();

        Container.Bind<IHavePressedEvent>().WithId(IdNames.Login).FromInstance(_loginView).AsCached();
        Container.Bind<IHavePressedEvent>().WithId(IdNames.Register).FromInstance(_registerView).AsCached();

        Container.Bind<EmailChecker>().AsSingle();
    }
}