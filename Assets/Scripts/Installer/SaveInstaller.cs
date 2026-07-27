using Zenject;
using UnityEngine;

public class SaveInstaller : MonoInstaller
{
    [SerializeField] private string _pathName;

    public override void InstallBindings()
    {
        string path = Application.persistentDataPath + "/" + _pathName + ".json";

        Container.Bind<ISaveReader>().To<SaveReader>().AsSingle().WithArguments(path);
        Container.Bind<ISaveWriter>().To<SaveWriter>().AsSingle().WithArguments(path);

        Container.BindInterfacesTo<SaveManager>().AsSingle();
    }
}