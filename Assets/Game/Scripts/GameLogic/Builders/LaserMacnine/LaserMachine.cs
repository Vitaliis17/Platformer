using UnityEngine;
using Zenject;
using R3;

public class LaserMachine : MonoBehaviour
{
    [Inject(Id = IdNames.Laser)] private IHaveActivity _laser;
    [Inject(Id = IdNames.Button)] private IHaveActivityWithEvent _button;

    [SerializeField] private Collider2D _collider;

    private void Start()
    {
        _collider.enabled = true;

        _button.Activated.Subscribe(_ => _laser.Deactivate()).AddTo(this);
        _button.Deactivated.Subscribe(_ => _laser.Activate()).AddTo(this);
    }
}