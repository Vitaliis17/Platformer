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
        => Container.BindInterfacesTo<PauseSwitcher>().AsSingle();
}