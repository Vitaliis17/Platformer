using UnityEngine;
using Zenject;

public class ApplicationInstaller : MonoInstaller
{
    [SerializeField] private PauseData _pauseData;

    public override void InstallBindings()
    {
        Container.Bind<PauseData>().FromScriptableObject(_pauseData).AsSingle();
        Container.Bind<PauseSwitcher>().FromComponentInHierarchy().AsSingle();

        Container.Bind<IPauseSwitcher>().FromMethod(ctx => ctx.Container.Resolve<PauseSwitcher>()).AsSingle();
        Container.Bind<IPauser>().FromMethod(ctx => ctx.Container.Resolve<PauseSwitcher>()).AsSingle();

        Container.Bind<Quiter>().FromComponentInHierarchy().AsSingle();
        Container.Bind<IQuiter>().FromMethod(ctx => ctx.Container.Resolve<Quiter>()).AsSingle();
    }
}