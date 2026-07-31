using System;

public interface ISceneLoader : IDisposable
{
    void LoadSceneAsync(SceneNames name);

    void LoadMenu();
}
