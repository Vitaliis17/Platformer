using UnityEngine;
using Zenject;
using System.Collections.Generic;
using System.Threading;

public class LevelSwitcherInstaller : MonoInstaller
{
    [SerializeField] private LevelSwitcher _levelSwitcher;

    public override void InstallBindings()
    {
        Container.Bind<Dictionary<int, SceneNames>>().FromInstance(new Dictionary<int, SceneNames>
        {
            { (int)SceneNames.FirstLevel, SceneNames.FirstLevel }
        }).AsSingle();

        Container.Bind<IContainerReceiverByIndex<SceneNames>>().To<SceneNamesContainer>().AsSingle();

        Container.Bind<CancellationTokenSource>().FromInstance(new()).AsTransient();
        Container.Bind<ISceneLoader>().To<SceneLoader>().AsTransient();

        BindLevelSwitcher();
    }

    private void BindLevelSwitcher()
    {
        Container.Bind<LevelSwitcher>().FromInstance(_levelSwitcher).AsSingle();

        Container.Bind<IMenuLoader>().FromMethod(ctx => ctx.Container.Resolve<LevelSwitcher>()).AsSingle();
        Container.Bind<INextLevelLoader>().FromMethod(ctx => ctx.Container.Resolve<LevelSwitcher>()).AsSingle();
    }
}