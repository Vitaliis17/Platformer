using NUnit.Framework;
using System.Collections.Generic;
using Zenject;

[TestFixture]
public class SceneNamesContainerTester
{
    private DiContainer _container;
    IContainerReceiverByIndex<SceneNames> _containerIndex;

    [SetUp]
    public void SetContainer()
    {
        _container = new();

        _container.Bind<Dictionary<int, SceneNames>>().FromInstance(new Dictionary<int, SceneNames>
        {
            { (int)SceneNames.FirstLevel, SceneNames.FirstLevel }
        });
        _container.Bind<IContainerReceiverByIndex<SceneNames>>().To<SceneNamesContainer>().AsSingle();

        _containerIndex = _container.Resolve<IContainerReceiverByIndex<SceneNames>>();
    }

    [TestCase(-1, SceneNames.None)]
    [TestCase(0, SceneNames.None)]
    [TestCase(1, SceneNames.FirstLevel)]
    public void TestGet(int index, SceneNames name)
    {
        SceneNames expectedName = _containerIndex.Get(index);

        Assert.AreEqual(name, expectedName);
    }
}