using R3;
using UnityEngine;

public class PlayerPresenter : MonoBehaviour
{
    [SerializeField] private GameplayAction _action;
    [SerializeField] private Player _player;

    private void Start()
        => _action.DirectionChanged.Where(direction => direction.sqrMagnitude != 0f).Subscribe(direction => _player.Move(direction)).AddTo(this);
}