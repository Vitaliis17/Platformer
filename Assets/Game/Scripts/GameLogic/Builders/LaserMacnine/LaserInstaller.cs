using Zenject;
using UnityEngine;

public class LaserInstaller : MonoInstaller
{
    [SerializeField] private Transform _laser;
    [SerializeField] private ScaleSwitcherData _data;

    public override void InstallBindings()
    {
        Container.Bind<IScaleSwitcherData>().FromInstance(_data).AsSingle();

        Container.Bind<ScaleSwitcher>().AsTransient().WithArguments(_laser);
        Container.Bind<IHaveActivity>().WithId(IdNames.Laser).To<ScaleSwitcher>().FromResolve();
    }
}