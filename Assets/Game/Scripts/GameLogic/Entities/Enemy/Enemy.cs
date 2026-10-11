using R3;
using UnityEngine;
using Zenject;

public class Enemy : MonoBehaviour
{
    [Inject(Id = IdNames.Horizontal)] private ITransportable _horizontalMover;
    [Inject(Id = IdNames.Vertical)] private ITransportable _verticalMover;

    [Inject(Id = IdNames.Movement)] private IHaveMultiplier _velocityData;

    private readonly Subject<Unit> _isHorizontalMoved = new();
    private readonly Subject<Unit> _isVerticalMoved = new();

    private Rigidbody2D _rigidbody;

    public Observable<Unit> IsHorizontalMoved => _isHorizontalMoved;
    public Observable<Unit> IsVerticalMoved => _isVerticalMoved;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();

        _rigidbody.freezeRotation = true;
    }

    public void SetVelocity(bool isLadder)
    {
        if (_rigidbody == null)
            return;

        float velocityX = _horizontalMover.TransferDelta().x;
        float velocityY = _verticalMover.TransferDelta().y;

        Vector2 velocity = Vector2.zero;

        if (isLadder)
            velocity = new Vector2(velocityX * _velocityData.Multiplier, velocityY * _velocityData.Multiplier);
        else
            velocity = new Vector2(velocityX * _velocityData.Multiplier, _rigidbody.linearVelocityY);

        _rigidbody.linearVelocity = velocity;
    }

    public void MoveHorizontal(float direction)
    {
        _horizontalMover.SetDelta(direction);

        _isHorizontalMoved.OnNext(Unit.Default);
    }

    public void MoveVertical(float direction)
    {
        _verticalMover.SetDelta(direction);

        _isVerticalMoved.OnNext(Unit.Default);
    }

    private void OnDestroy()
    {
        _isVerticalMoved?.Dispose();
        _isHorizontalMoved?.Dispose();
    }
}