using R3;
using UnityEngine;
using Zenject;

[RequireComponent(typeof(Rigidbody2D))]
public class Player : MonoBehaviour, IHavePosition, IMovable, IMovableEvents
{
    [Inject(Id = IdNames.Horizontal)] private ITransportable _horizontalMover;
    [Inject(Id = IdNames.Vertical)] private ITransportable _verticalMover;
    [Inject(Id = TriggerNames.Ladder)] private IHaveTriggerEvent _event;

    [Inject] private IJumpable _jumper;

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

    private void FixedUpdate()
    {
        float velocityX = _horizontalMover.TransferDelta().x;
        float velocityY = _verticalMover.TransferDelta().y;

        if (_event.HaveTriggered())
            _rigidbody.linearVelocity = new Vector2(velocityX * 40, velocityY * 30);
        else
            _rigidbody.linearVelocity = new Vector2(velocityX * 40, _rigidbody.linearVelocityY);
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