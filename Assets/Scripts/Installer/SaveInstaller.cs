using Zenject;
using UnityEngine;

public class SaveInstaller : MonoInstaller
{
    [SerializeField] private string _pathName;

    [SerializeField] private SaveManager _saveManager;
    [SerializeField] private LevelUnlocker _levelUnlocker;

    public override void InstallBindings()
    {
        string path = Application.persistentDataPath + "/" + _pathName + ".json";

        Container.Bind<ISaveReader>().To<SaveReader>().AsSingle().WithArguments(path);
        Container.Bind<ISaveWriter>().To<SaveWriter>().AsSingle().WithArguments(path);

        Container.Bind<SaveManager>().FromInstance(_saveManager).AsSingle();
        Container.Bind<ISaveManager>().FromMethod(ctx => ctx.Container.Resolve<SaveManager>()).AsSingle();

        Container.Bind<LevelUnlocker>().FromInstance(_levelUnlocker).AsSingle();
        Container.Bind<ILevelUnlocker>().FromMethod(ctx => ctx.Container.Resolve<LevelUnlocker>()).AsSingle();
    }
}