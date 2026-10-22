using UnityEngine;
using Zenject;

public class DatabaseInitializeInstaller : MonoInstaller
{
    [SerializeField] private FirebaseSaveLoaderView _saveView;
    [SerializeField] private FirebaseSaveLoaderView _cancelView;
    
    [SerializeField] private PanelActivater _activater;
    [SerializeField] private LoadingCompleter _completer;

    public override void InstallBindings()
    {
        Container.BindInterfacesTo<DatabaseInitializer>().AsSingle();
        Container.BindInterfacesTo<FirebaseSaveLoader>().AsSingle();

        Container.Bind<IHaveOnClickEvent>().WithId(ViewNames.SaveClick).FromInstance(_saveView).AsCached();
        Container.Bind<IHaveOnClickEvent>().WithId(ViewNames.CancelClick).FromInstance(_cancelView).AsCached();

        Container.BindInterfacesTo<FirebaseSaveLoaderPresenter>().AsSingle();

        Container.Bind<IActivater>().FromInstance(_activater).AsSingle();
        Container.BindInterfacesTo<PanelActivatePresenter>().AsSingle();

        Container.BindInterfacesTo<LoadingCompleter>().FromInstance(_completer).AsSingle();
    }
}