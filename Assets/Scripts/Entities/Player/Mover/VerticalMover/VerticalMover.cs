using UnityEngine;
using Zenject;

public class VerticalMover : ITransferable
{
    [Inject] private readonly MoverData _data;
    [Inject] private Rigidbody2D _rigidbody;

    private Vector2 _delta;

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
