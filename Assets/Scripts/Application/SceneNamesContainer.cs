using System.Collections.Generic;

public class SceneNamesContainer
{
    private readonly Dictionary<int, SceneNames> _sceneNames;

    public SceneNamesContainer(Dictionary<int, SceneNames> sceneNames)
        => _sceneNames = sceneNames;

    public SceneNames GetName(int index)
    {
        if(_sceneNames.TryGetValue(index, out SceneNames sceneName))
            return sceneName;

        return SceneNames.None;
    }
}
