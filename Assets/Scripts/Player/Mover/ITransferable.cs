using UnityEngine;

public interface ITransferable
{
    void SetDelta(float direction);

    Vector2 TransferDelta();
}