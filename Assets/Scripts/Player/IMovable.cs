using UnityEngine;

public interface IMoveable
{
    void SetDelta(float direction);

    Vector2 TransferDelta();

    void Initialize(Rigidbody2D rigidbody);
}