using Zenject;
using System.Collections.Generic;
using System.Threading;

public class Installer : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<Dictionary<int, SceneNames>>().FromInstance(new Dictionary<int, SceneNames>
        {
            { 0, SceneNames.FirstLevel }
        }).AsSingle();

        Container.Bind<IContainerReceiver<SceneNames>>().To<SceneNamesContainer>().AsSingle();

        Container.Bind<CancellationTokenSource>().FromInstance(new()).AsTransient();
        Container.Bind<ISceneLoader>().To<SceneLoader>().AsTransient();
    }
}