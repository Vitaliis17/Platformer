using UnityEngine;
using Zenject;

public class UIInstaller : MonoInstaller
{
    [SerializeField] private PauseData _pauseData;

    public override void InstallBindings()
    {
        Container.Bind<PauseData>().FromScriptableObject(_pauseData).AsSingle();
        Container.Bind<PauseSwitcher>().FromComponentInHierarchy().AsSingle();
        Container.Bind<IPauseSwitcher>().FromMethod(ctx => ctx.Container.Resolve<PauseSwitcher>()).AsSingle();
    }
}