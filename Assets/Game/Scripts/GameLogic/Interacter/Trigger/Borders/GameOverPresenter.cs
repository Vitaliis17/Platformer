using UnityEngine;
using Zenject;
using System;
using R3;

public class GameOverPresenter : IInitializable, IDisposable
{
    private readonly Transform _gameOverPanel;

    [Inject(Id = TriggerNames.Border)] private IHaveTriggerEvent _trigger;
    [Inject] private IPauser _pauser;

    private DisposableBag _bag;

    [Inject]
    public GameOverPresenter(Transform gameOverPanel)
        => _gameOverPanel = gameOverPanel;

    public void Initialize()
    {
        Observable<bool> observable = _trigger.IsTriggered.Where(isTrigger => isTrigger);

        _bag.Add(observable.Subscribe(isTrigger => _gameOverPanel.gameObject.SetActive(isTrigger)));
        _bag.Add(observable.Subscribe(_ => _pauser.Pause()));
    }

    public void Dispose()
        => _bag.Dispose();
}