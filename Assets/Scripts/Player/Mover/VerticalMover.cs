using UnityEngine;
using Zenject;

public class VerticalMover : IMoveable
{
    private readonly MoverData _data;

    private Rigidbody2D _rigidbody;
    private Vector2 _delta;

    [Inject]
    public VerticalMover(MoverData data)
        => _data = data;

    public void Initialize(Rigidbody2D rigidbody)
        => _rigidbody = rigidbody;

    public void SetDelta(float direction)
    {
        if (_rigidbody == null)
            return;

        _delta = Vector2.up * direction * _data.Speed * Time.fixedDeltaTime;
    }

    public Vector2 TransferDelta()
    {
        Vector2 delta = _delta;

        _delta = Vector2.zero;

        return delta;
    }
}
