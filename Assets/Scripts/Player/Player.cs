using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Player : MonoBehaviour, IMoveable
{
    [SerializeField, Min(0)] private float _speed;

    private IMoveable _mover;

    private void Awake()
    {
        Rigidbody2D rigidbody = GetComponent<Rigidbody2D>();
        _mover = new Mover(_speed, rigidbody);
    }

    public void Move(Vector2 direction)
        => _mover.Move(direction);
}