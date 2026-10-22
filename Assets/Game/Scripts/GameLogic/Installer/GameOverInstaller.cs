using Zenject;
using UnityEngine;

public class GameOverInstaller : MonoInstaller
{
    [SerializeField] private Transform _gameOverPanel;

    public override void InstallBindings()
        => Container.BindInterfacesTo<GameOverPresenter>().AsSingle().WithArguments(_gameOverPanel);
}