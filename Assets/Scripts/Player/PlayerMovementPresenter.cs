using R3;
using UnityEngine;
using Zenject;

public class PlayerMovementPresenter : MonoBehaviour
{
    [Inject] private IMovementReader _movementReader;
    [Inject] private IJumpReader _jumpReader;

    [Inject] private IMovable _player;

    [Inject(Id = TriggerNames.Ladder)] private IHaveTriggerEvent _ladderMap;

    private void Start()
    {
        const float VerticalOffset = 0.5f;

        Observable<Vector2> observable = _movementReader.DirectionChanged.Where(direction => direction.sqrMagnitude != 0f);

        observable.Subscribe(direction => _player.MoveHorizontal(direction.x)).AddTo(this);

        observable.Where(_ => _ladderMap.IsTriggered.CurrentValue)
            .Where(direction => Mathf.Abs(direction.y) > VerticalOffset)
            .Subscribe(direction => _player.MoveVertical(direction.y)).AddTo(this);

        _jumpReader.Jumped.Subscribe(_ => _player.Jump()).AddTo(this);
    }
}