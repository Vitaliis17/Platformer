using UnityEngine;

public interface IHaveRigidbody
{
    Rigidbody2D Rigidbody { get; }

    void DisablePhysics();

    void EnablePhysics();
}