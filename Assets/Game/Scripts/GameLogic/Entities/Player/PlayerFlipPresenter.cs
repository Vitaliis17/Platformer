using R3;
using System;
using UnityEngine;
using Zenject;

public class PlayerFlipPresenter : IInitializable, IDisposable
{
    private readonly SpriteRenderer _playerRenderer;

    [Inject] private IMovementReader _reader;
    [Inject] private IFlipper _flipper;

    private float _currentSign = 1;

    private DisposableBag _bag;

    [Inject]
    public PlayerFlipPresenter(SpriteRenderer playerRenderer)
        => _playerRenderer = playerRenderer;

    public void Initialize()
    {
        Observable<Vector2> observable = _reader.Moved.Where(direction => direction.sqrMagnitude != 0f)
            .Where(direction => Mathf.Sign(direction.x) != Mathf.Sign(_currentSign));

        _bag.Add(observable.Subscribe(_ => _flipper.FlipX(_playerRenderer)));
        _bag.Add(observable.Subscribe(direction => _currentSign = direction.x));
    }

    public void Dispose()
        => _bag.Dispose();
}
