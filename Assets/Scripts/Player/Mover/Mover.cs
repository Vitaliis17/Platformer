using UnityEngine;

public class Mover : IMoveable
{
    private readonly float _speed;

    private readonly Rigidbody2D _rigidbody;
    private readonly Transform _transform;

    public Mover(float speed, Rigidbody2D rigidbody)
    {
        if (speed > 0f)
            _speed = speed;

        _rigidbody = rigidbody;
        _transform = rigidbody.transform;
    }

    public void Move(Vector2 direction)
        => _rigidbody.MovePosition((Vector2)_transform.position + direction * _speed * Time.fixedDeltaTime);
}
