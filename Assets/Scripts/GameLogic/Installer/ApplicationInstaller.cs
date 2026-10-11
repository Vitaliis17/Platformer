using UnityEngine;
using Zenject;

public class ApplicationInstaller : MonoInstaller
{
    [SerializeField] private PauseData _pauseData;

    public override void InstallBindings()
    {
        BindPauseData();
        BindPauseSwitcher();
    }

    private void BindPauseData()
    {
        Container.Bind<PauseData>().FromScriptableObject(_pauseData).AsSingle();
        Container.Bind<IGameSpeedSender>().FromMethod(ctx => ctx.Container.Resolve<PauseData>()).AsSingle();
    }

    private void BindPauseSwitcher()
    {
        Container.Bind<PauseSwitcher>().AsSingle();
        Container.Bind<IPauseSwitcher>().FromMethod(ctx => ctx.Container.Resolve<PauseSwitcher>()).AsSingle();
        Container.Bind<IPauser>().FromMethod(ctx => ctx.Container.Resolve<PauseSwitcher>()).AsSingle();
        Container.Bind<IUnpauser>().FromMethod(ctx => ctx.Container.Resolve<PauseSwitcher>()).AsSingle();
    }
}