using UnityEngine;
using Zenject;

public class Mover : ITransportable
{
    private readonly MoverData _data;
    private readonly Rigidbody2D _rigidbody;

    private readonly Vector2 _direction;

    private Vector2 _delta;

    [Inject]
    public Mover(MoverData data, Rigidbody2D rigidbody, Vector2 direction)
    {
        _data = data;
        _rigidbody = rigidbody;
        _direction = direction;
    }

    public void SetDelta(float direction)
    {
        if (_rigidbody == null)
            return;

        _delta = _direction * direction * _data.Speed * Time.fixedDeltaTime;
    }

    public Vector2 TransferDelta()
    {
        Vector2 delta = _delta;

        _delta = Vector2.zero;

        return delta;
    }
}
