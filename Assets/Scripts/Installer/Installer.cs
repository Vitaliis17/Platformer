using Zenject;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Installer : MonoInstaller
{
    [SerializeField] private MoverData _moverData;

    public override void InstallBindings()
    {
        Container.Bind<Dictionary<int, SceneNames>>().FromInstance(new Dictionary<int, SceneNames>
        {
            { 0, SceneNames.FirstLevel }
        }).AsSingle();

        Container.Bind<IContainer<SceneNames>>().To<SceneNamesContainer>().AsSingle();

        Container.Bind<CancellationTokenSource>().FromInstance(new()).AsTransient();
        Container.Bind<ISceneLoader>().To<SceneLoader>().AsTransient();

        Container.Bind<MoverData>().FromScriptableObject(_moverData).AsSingle();

        Container.Bind<IMoveable>().WithId("Horizontal").To<HorizontalMover>().AsTransient();
        Container.Bind<IMoveable>().WithId("Vertical").To<VerticalMover>().AsTransient();
        
        Container.Bind<Player>().FromComponentInHierarchy().AsSingle();
    }
}
