using UnityEngine;
using Zenject;

public class Mover : IMoveable
{
    private readonly MoverData _data;

    private Rigidbody2D _rigidbody;

    [Inject]
    public Mover(MoverData data)
        => _data = data;

    public void Initialize(Rigidbody2D rigidbody)
        => _rigidbody = rigidbody;

    public void Move(Vector2 direction)
    {
        if(_rigidbody == null) 
            return;

        _rigidbody.MovePosition((Vector2)_rigidbody.transform.position + direction * _data.Speed * Time.fixedDeltaTime);
    }
}
