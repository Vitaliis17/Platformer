using Zenject;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class LevelSwitcherInstaller : MonoInstaller
{
    [SerializeField] private LevelData _levelData;

    public override void InstallBindings()
    {
        BindSceneLoader();
        BindLevelLoader();

        BindLevelData();
    }

    private void BindSceneLoader()
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
    }

    private void BindLevelLoader()
    {
        Container.Bind<LevelLoader>().AsSingle();
        Container.Bind<IMenuLoader>().FromMethod(ctx => ctx.Container.Resolve<LevelLoader>()).AsSingle();
        Container.Bind<ILevelLoader>().FromMethod(ctx => ctx.Container.Resolve<LevelLoader>()).AsSingle();
        Container.Bind<IHaveLevelLoaderEvent>().FromMethod(ctx => ctx.Container.Resolve<LevelLoader>()).AsSingle();
    }

    private void BindLevelData()
    {
        Container.Bind<LevelData>().FromInstance(_levelData).AsSingle();
        Container.Bind<ICurrentLevelSetter>().FromMethod(ctx => ctx.Container.Resolve<LevelData>()).AsSingle();
    }
}