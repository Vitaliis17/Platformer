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
            { (int)SceneNames.SecondLevel, SceneNames.SecondLevel }
        }).AsSingle();

        Container.Bind<IContainerReceiverByIndex<SceneNames>>().To<SceneNamesContainer>().AsSingle();

        Container.Bind<CancellationTokenSource>().FromInstance(new()).AsTransient();
        Container.Bind<ISceneLoader>().To<SceneLoader>().AsTransient();
    }

    private void BindLevelLoader()
        => Container.BindInterfacesTo<LevelLoader>().AsSingle();

    private void BindLevelData()
        => Container.BindInterfacesTo<LevelData>().FromInstance(_levelData).AsSingle();
}