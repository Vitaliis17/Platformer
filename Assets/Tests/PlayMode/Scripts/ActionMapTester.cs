using UnityEngine;
using NUnit.Framework;
using UnityEngine.TestTools;
using System.Collections;
using Zenject;
using UnityEngine.SceneManagement;
using System.Threading;

[TestFixture]
public class ActionMapTester 
{
    private DiContainer _container;
    private IActionMap _actionMap;

    private ISceneLoader _levelSwitcher;

    [UnitySetUp]
    public IEnumerator SetStart()
    {
        _container = new();

        _container.Bind<CancellationTokenSource>().FromInstance(new()).AsTransient();
        _container.Bind<ISceneLoader>().To<SceneLoader>().AsTransient();
        _levelSwitcher = _container.Resolve<ISceneLoader>();

        _levelSwitcher.LoadSceneAsync(SceneNames.FirstLevel);

        yield return new WaitUntil(() => SceneManager.GetActiveScene().name == "FirstLevel");
        _actionMap = Object.FindAnyObjectByType<GameplayAction>();
    }

    [UnityTest]
    public IEnumerator TestIsNotNull()
    {
        Assert.IsNotNull(_actionMap);

        yield return null;
    }

    [UnityTest]
    public IEnumerator TestType()
    {
        Assert.IsInstanceOf<ActionMap>(_actionMap);

        yield return null;
    }

    [UnityTest]
    public IEnumerator Te()
    {
        Assert.IsTrue(true);

        yield return null;
    }
}
