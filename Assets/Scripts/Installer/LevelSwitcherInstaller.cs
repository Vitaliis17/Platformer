using UnityEngine;
using Zenject;
using System.Collections.Generic;
using System.Threading;

public class LevelSwitcherInstaller : MonoInstaller
{
    [SerializeField] private LevelLoader _levelLoader;

    public override void InstallBindings()
    {
        Container.Bind<Dictionary<int, SceneNames>>().FromInstance(new Dictionary<int, SceneNames>
        {
            { (int)SceneNames.FirstLevel, SceneNames.FirstLevel },
            { (int)SceneNames.SecondLevel, SceneNames.SecondLevel },
            { (int)SceneNames.ThirdLevel, SceneNames.ThirdLevel }
        }).AsSingle();

        Container.Bind<IContainerReceiverByIndex<SceneNames>>().To<SceneNamesContainer>().AsSingle();

        Container.Bind<CancellationTokenSource>().FromInstance(new()).AsTransient();
        Container.Bind<ISceneLoader>().To<SceneLoader>().AsTransient();

        BindLevelSwitcher();
    }

    private void BindLevelSwitcher()
    {
        Container.Bind<LevelLoader>().FromInstance(_levelLoader).AsSingle();

        Container.Bind<IMenuLoader>().FromMethod(ctx => ctx.Container.Resolve<LevelLoader>()).AsSingle();
        Container.Bind<ILevelLoader>().FromMethod(ctx => ctx.Container.Resolve<LevelLoader>()).AsSingle();
        Container.Bind<IHaveLevelLoaderEvent>().FromMethod(ctx => ctx.Container.Resolve<LevelLoader>()).AsSingle();
    }
}