using R3;
using UnityEngine;

public class PlayerPresenter : MonoBehaviour
{
    [SerializeField] private GameplayAction _action;
    [SerializeField] private Player _player;
    [SerializeField] private TriggerTiles _triggerTiles;

    private bool _isTilesTriggered;

    private void Start()
    {
        _triggerTiles.IsTriggered.Subscribe(isTrigger => _isTilesTriggered = isTrigger).AddTo(this);

        Observable<Vector2> observable = _action.DirectionChanged.Where(direction => direction.sqrMagnitude != 0f);
        
        observable.Subscribe(direction => _player.MoveHorizontal(direction.x)).AddTo(this);
        observable.Where(_ => _isTilesTriggered).Subscribe(direction => _player.MoveVertical(direction.y)).AddTo(this);
    }
}