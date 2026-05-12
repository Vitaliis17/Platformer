using UnityEngine;
using Zenject;

public class Jumper : IJumpable
{
    private readonly JumpData _data;

    private Rigidbody2D _rigidbody;

    [Inject]
    public Jumper(JumpData data)
        => _data = data;

    public void Initialize(Rigidbody2D rigidbody)
        => _rigidbody = rigidbody;

    public void Jump()
        => _rigidbody.AddForce(_data.Force);
}
