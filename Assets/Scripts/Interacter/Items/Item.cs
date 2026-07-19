using UnityEngine;
using Zenject;

[RequireComponent(typeof(Rigidbody2D), typeof(Collider2D))]
public abstract class Item : MonoBehaviour, ITransferable
{
    private IRigidbodyData _data;
    private Collider2D _collider;

    public Vector2 Position => transform.position;

    public Rigidbody2D Rigidbody { get; private set; }

    public void Awake()
    {
        _collider = GetComponent<Collider2D>();
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

    public void TurnOnTrigger()
        => _collider.isTrigger = true;

    public void TurnOffTrigger()
        => _collider.isTrigger = false;
}
