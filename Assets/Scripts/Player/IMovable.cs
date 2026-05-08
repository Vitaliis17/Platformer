using UnityEngine;

public interface IMoveable
{
    void Move(Vector2 direction);

    void Initialize(Rigidbody2D rigidbody);
}