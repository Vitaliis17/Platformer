using UnityEngine;
using Zenject;

[RequireComponent(typeof(Rigidbody2D))]
public class Player : MonoBehaviour
{
    [Inject] private IMoveable _mover;

    private void Start()
    {
        Rigidbody2D rigidbody = GetComponent<Rigidbody2D>();
        rigidbody.freezeRotation = true;

        _mover.Initialize(rigidbody);
    }

    public void Move(Vector2 direction)
        => _mover.Move(direction);
}