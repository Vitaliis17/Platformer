using Zenject;
using UnityEngine;

public class LevelUnlockerInstaller : MonoInstaller
{
    [SerializeField] private LevelUnlocker _levelUnlocker;

    public override void InstallBindings()
    {
        Container.Bind<LevelUnlocker>().FromInstance(_levelUnlocker).AsSingle();
        Container.Bind<ILevelUnlocker>().FromMethod(ctx => ctx.Container.Resolve<LevelUnlocker>()).AsSingle();
        //Container.Bind<ILevelUnlocker>().To<LevelUnlocker>().AsSingle();
    }
}