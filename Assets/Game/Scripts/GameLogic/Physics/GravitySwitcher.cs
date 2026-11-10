using UnityEngine;

public class GravitySwitcher : MonoBehaviour, IGravitySwitcher
{
    [SerializeField] private Rigidbody2D _rigidbody;

    private float _maxGravity;
    private int _minGravity;

    private void Awake()
    {
        const int NoGravityScale = 0;

        _maxGravity = _rigidbody.gravityScale;
        _minGravity = NoGravityScale;
    }

    public void TurnOnGravity()
        => _rigidbody.gravityScale = _maxGravity;

    public void TurnOffGravity()
        => _rigidbody.gravityScale = _minGravity;
}
