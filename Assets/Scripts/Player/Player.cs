using R3;
using UnityEngine;
using Zenject;

[RequireComponent(typeof(Rigidbody2D))]
public class Player : MonoBehaviour, IHavePosition, IMovable, IMovableEvents, IVelocitySetter
{
    [Inject(Id = IdNames.Horizontal)] private ITransportable _horizontalMover;
    [Inject(Id = IdNames.Vertical)] private ITransportable _verticalMover;

    [Inject] private IJumpable _jumper;

    [Inject] private IVelocityData _velocityData;

    private readonly Subject<Unit> _isHorizontalMoved = new();
    private readonly Subject<Unit> _isVerticalMoved = new();

    private readonly Subject<Unit> _isJumped = new();

    private Rigidbody2D _rigidbody;

    public Vector2 Position => transform.position;

    public Observable<Unit> IsHorizontalMoved => _isHorizontalMoved;
    public Observable<Unit> IsVerticalMoved => _isVerticalMoved;

    public Observable<Unit> IsJumped => _isJumped;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();

        _rigidbody.freezeRotation = true;
    }

    public void SetVelocity(bool isLadder)
    {
        float velocityX = _horizontalMover.TransferDelta().x;
        float velocityY = _verticalMover.TransferDelta().y;

        if (isLadder)
            _rigidbody.linearVelocity = new Vector2(velocityX * _velocityData.Multiplier, velocityY * _velocityData.Multiplier);
        else
            _rigidbody.linearVelocity = new Vector2(velocityX * _velocityData.Multiplier, _rigidbody.linearVelocityY);
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

    public void Jump()
    {
        _jumper.Jump();

        _isJumped.OnNext(Unit.Default);
    }

    private void OnDestroy()
    {
        _isVerticalMoved?.Dispose();
        _isHorizontalMoved?.Dispose();
        _isJumped?.Dispose();
    }
}