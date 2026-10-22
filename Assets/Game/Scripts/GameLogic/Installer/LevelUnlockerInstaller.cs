using Zenject;
using UnityEngine;
using UnityEngine.UI;

public class LevelUnlockerInstaller : MonoInstaller
{
    [SerializeField] private Button[] _levelButtons;

    public override void InstallBindings()
    {
        Container.BindInterfacesTo<LevelUnlocker>().AsSingle().WithArguments(_levelButtons);
        Container.BindInterfacesTo<LevelUnlockerPresenter>().AsSingle();
    }
}