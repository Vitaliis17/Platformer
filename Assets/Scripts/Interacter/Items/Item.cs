using UnityEngine;
using Zenject;

[RequireComponent(typeof(Rigidbody2D), typeof(BoxCollider2D))]
public abstract class Item : MonoBehaviour, ITransferable
{
    private IRigidbodyData _data;

    public Vector2 Position => transform.position;

    public BoxCollider2D Collider { get; private set; }
    public Rigidbody2D Rigidbody { get; private set; }

    public void Awake()
    {
        Collider = GetComponent<BoxCollider2D>();
        Rigidbody = GetComponent<Rigidbody2D>();
    }

    [Inject]
    private void Construct(IRigidbodyData data)
        => _data = data;

    public void DisablePhysics()
    {
        Rigidbody.gravityScale = _data.MinGravity;
        Rigidbody.mass = _data.MinMass;
    }

    public void EnablePhysics()
    {
        Rigidbody.gravityScale = _data.BaseGravity;
        Rigidbody.mass = _data.BaseMass;
    }
}
