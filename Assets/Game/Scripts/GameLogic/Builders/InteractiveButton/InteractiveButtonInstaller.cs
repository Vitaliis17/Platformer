using System;
using UnityEngine;
using Zenject;

public class InteractiveButtonInstaller : MonoInstaller
{
    [SerializeField] private ButtonData _data;
    [SerializeField] private Transform _button;

    public override void InstallBindings()
    {
        Container.Bind<IButtonData>().FromInstance(_data).AsSingle();

        Container.Bind<ButtonActivitySwitcher>().AsSingle().WithArguments(_button);
        Container.Bind<IHaveActivityWithEvent>().WithId(IdNames.Button).To<ButtonActivitySwitcher>().FromResolve();
        Container.Bind<IDisposable>().To<ButtonActivitySwitcher>().FromResolve();
    }
}