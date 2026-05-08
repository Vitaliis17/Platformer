using System.Collections.Generic;
using Zenject;

public class SceneNamesContainer : IContainer<SceneNames>
{
    private readonly Dictionary<int, SceneNames> _sceneNames;

    [Inject]
    public SceneNamesContainer(Dictionary<int, SceneNames> sceneNames)
        => _sceneNames = sceneNames;

    public SceneNames Get(int index)
    {
        if(_sceneNames.TryGetValue(index, out SceneNames sceneName))
            return sceneName;

        return SceneNames.None;
    }
}
