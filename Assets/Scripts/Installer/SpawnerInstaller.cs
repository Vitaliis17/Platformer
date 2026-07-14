using UnityEngine;
using Zenject;

public class SpawnerInstaller : MonoInstaller
{
    [SerializeField] private Transform _container;
    [SerializeField] private Cloud _cloud;

    [SerializeField] private SpriteData _spriteCloudData;

    public override void InstallBindings()
        => Container.Bind<ISpawner<Cloud>>().To<CloudSpawner>().AsSingle().WithArguments(_spriteCloudData, _container, _cloud);
}