using UnityEngine;
using Zenject;

public class Jumper : IJumpable
{
    [Inject] private JumpData _data;
    [Inject] private Rigidbody2D _rigidbody;

    public void Jump()
        => _rigidbody.AddForce(_data.Force);
}
