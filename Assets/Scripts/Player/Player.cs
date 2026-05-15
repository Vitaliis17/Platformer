using UnityEngine;
using Zenject;

[RequireComponent(typeof(Rigidbody2D))]
public class Player : MonoBehaviour, IHavePosition
{
    [Inject(Id = IdNames.Horizontal)] private IMoveable _horizontalMover;
    [Inject(Id = IdNames.Vertical)] private IMoveable _verticalMover;

    [Inject] private IJumpable _jumper;

    private Rigidbody2D _rigidbody;

    public Vector2 Position => transform.position;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _rigidbody.freezeRotation = true;

        _horizontalMover.Initialize(_rigidbody);
        _verticalMover.Initialize(_rigidbody);

        _jumper.Initialize(_rigidbody);
    }

    private void FixedUpdate()
    {
        Vector2 nextPosition = (Vector2)transform.position + _horizontalMover.TransferDelta() + _verticalMover.TransferDelta();
        _rigidbody.MovePosition(nextPosition);
    }

    public void MoveHorizontal(float direction)
        => _horizontalMover.SetDelta(direction);

    public void MoveVertical(float direction)
        => _verticalMover.SetDelta(direction);

    public void Jump()
        => _jumper.Jump();
}