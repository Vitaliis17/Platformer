using UnityEngine;
using Zenject;

public class Jumper : IJumpable
{
    [Inject(Id = IdNames.Jumping)] private IHaveMultiplier _multiplierHaver;
    private readonly Rigidbody2D _rigidbody;

    public Jumper(Rigidbody2D rigidbody)
        => _rigidbody = rigidbody;

    public void Jump()
    {
        const int NoSpeed = 0;

        _rigidbody.linearVelocity += new Vector2(NoSpeed, _multiplierHaver.Multiplier);
    }
}
