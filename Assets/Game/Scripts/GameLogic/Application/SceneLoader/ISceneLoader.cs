using System;
using UnityEngine.SceneManagement;
using Cysharp.Threading.Tasks;

public interface ISceneLoader : IDisposable
{
    UniTask LoadSceneAsync(SceneNames name, LoadSceneMode loadMode = LoadSceneMode.Single);

    void LoadMenu();
}
