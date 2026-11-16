using Zenject;
using UnityEngine;
using System;

public class LaserInstaller : MonoInstaller
{
    [SerializeField] private Transform _laser;

    public override void InstallBindings()
    {
        Container.Bind<ScaleSwitcher>().AsSingle().WithArguments(_laser);
        Container.Bind<IHaveActivity>().WithId(IdNames.Laser).To<ScaleSwitcher>().FromResolve();
    }
}