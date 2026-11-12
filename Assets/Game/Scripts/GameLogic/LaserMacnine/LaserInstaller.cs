using Zenject;
using UnityEngine;

public class LaserInstaller : MonoInstaller
{
    [SerializeField] private Transform _laser;

    public override void InstallBindings()
    {
        Container.Bind<IHaveActivity>().To<ScaleSwitcher>().AsSingle().WithArguments(_laser);
    }
}