using UnityEngine;
using Zenject;

public class Jumper : IJumpable
{
    private readonly JumpData _data;
    private readonly Rigidbody2D _rigidbody;

    [Inject]
    public Jumper(JumpData data, Rigidbody2D rigidbody)
    {
        _data = data;
        _rigidbody = rigidbody;
    }

    public void Jump()
        => _rigidbody.linearVelocity += new Vector2(0, 5);
}
