using R3;
using System;
using UnityEngine;
using Zenject;

public class PlayerMovementController : IInitializable, IDisposable
{
    [Inject] private IMovementReader _movementReader;
    [Inject] private IJumpReader _jumpReader;

    [Inject] private IMovable _player;
    [Inject] private IVelocitySetter _setter;

    [Inject(Id = TriggerNames.GroundChecker)] private IHaveTriggerEvent _groundChecker;

    [Inject(Id = TriggerNames.Ladder)] private IHaveTriggerEvent _ladderMap;

    private DisposableBag _bag;

    public void Initialize()
    {
        const float VerticalOffset = 0.5f;
        const float NoMovement = 0f;

        Observable<Vector2> observable = _movementReader.Moved.Where(direction => direction.sqrMagnitude != NoMovement);

        _bag.Add(observable.Subscribe(direction => _player.MoveHorizontal(direction.x)));

        _bag.Add(observable.Where(_ => _ladderMap.IsTriggered.CurrentValue)
            .Where(direction => Mathf.Abs(direction.y) > VerticalOffset)
            .Subscribe(direction => _player.MoveVertical(direction.y)));

        _bag.Add(_jumpReader.Jumped
            .Where(_ => _groundChecker.HaveTriggered())
            .Subscribe(_ => _player.Jump()));

        Observable.Interval(TimeSpan.FromSeconds(Time.fixedDeltaTime))
            .Subscribe(_ => _setter.SetVelocity(_ladderMap.HaveTriggered()));
    }

    public void Dispose()
        => _bag.Dispose();
}