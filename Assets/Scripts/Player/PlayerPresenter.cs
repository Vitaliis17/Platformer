using R3;
using UnityEngine;
using Zenject;

public class PlayerPresenter : MonoBehaviour
{
    [Inject] private IMovementReader _movementReader;
    [Inject] private IJumpReader _jumpReader;

    [SerializeField] private Player _player;
    [SerializeField] private MapTrigger _itemMap;
    [SerializeField] private MapTrigger _ladderMap;

    private bool _isTilesTriggered;

    private void Start()
    {
        _ladderMap.IsTriggered.Subscribe(isTrigger => _isTilesTriggered = isTrigger).AddTo(this);
        Observable<Vector2> observable = _movementReader.DirectionChanged.Where(direction => direction.sqrMagnitude != 0f);

        observable.Subscribe(direction => _player.MoveHorizontal(direction.x)).AddTo(this);
        observable.Where(_ => _isTilesTriggered).Subscribe(direction => _player.MoveVertical(direction.y)).AddTo(this);

        _jumpReader.Jumped.Subscribe(_ => _player.Jump()).AddTo(this);
    }
}