using UnityEngine;

public interface ITransportable
{
    void SetDelta(float direction);

    Vector2 TransferDelta();
}