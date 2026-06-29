using UnityEngine;

public interface ITransferable
{
    void SetDelta(float direction);

    void Initialize(Rigidbody2D rigidbody);

    Vector2 TransferDelta();
}