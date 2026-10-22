using UnityEngine;
using Zenject;

public class GravitySwitcher : IInitializable, IGravitySwitcher
{
    private readonly Rigidbody2D _rigidbody;

    private float _maxGravity;
    private int _minGravity;

    [Inject]
    public GravitySwitcher(Rigidbody2D rigidbody)
        => _rigidbody = rigidbody;

    public void Initialize()
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
