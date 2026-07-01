using R3;
using UnityEngine;
using Zenject;
using System;

public class PlayerFlipPresenter : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _playerRenderer;

    [Inject] private IMovementReader _reader;
    [Inject] private IFlipper _flipper;

    private int currentSign = 1;

    private void Start()
    {
        Observable<Vector2> observable = _reader.DirectionChanged.Where(direction => direction.sqrMagnitude != 0f)
            .Where(direction => Mathf.Sign(direction.x) != currentSign);

        observable.Subscribe(_ => _flipper.FlipX(_playerRenderer)).AddTo(this);
        observable.Subscribe(direction => currentSign = Math.Sign(direction.x)).AddTo(this);
    }
}
