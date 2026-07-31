using UnityEngine;
using Zenject;

public class SpawnerInstaller : MonoInstaller
{
    [SerializeField] private Transform _container;
    [SerializeField] private Cloud _cloud;

    [SerializeField] private SpriteData _spriteCloudData;
    [SerializeField] private SpeedData _speedData;

    [SerializeField] private CloudSpawnData _spawnData;

    public override void InstallBindings()
    {
        BindCloudSpawner();
        BindSpawnData();
    }

    private void BindCloudSpawner()
    {
        Container.Bind<SpeedData>().FromInstance(_speedData).AsSingle();
        Container.Bind<ISpeedRandomizer>().FromMethod(ctx => ctx.Container.Resolve<SpeedData>()).AsSingle();

        Container.Bind<SpriteData>().FromInstance(_spriteCloudData).AsSingle();
        Container.Bind<ISpriteRandomizer>().FromMethod(ctx => ctx.Container.Resolve<SpriteData>()).AsSingle();

        Container.BindFactory<Cloud, Factory<Cloud>>().FromComponentInNewPrefab(_cloud);
        Container.Bind<ISpawner<Cloud>>().To<CloudSpawner>().AsSingle().WithArguments(_container);
    }

    private void BindSpawnData()
    {
        Container.Bind<CloudSpawnData>().FromInstance(_spawnData).AsSingle();
        Container.Bind<ICloudSpawnData>().FromMethod(ctx => ctx.Container.Resolve<CloudSpawnData>()).AsSingle();
    }
}