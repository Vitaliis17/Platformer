using UnityEngine;
using Zenject;

public class LaserMachine : MonoBehaviour
{
    [Inject(Id = IdNames.Laser)] private IHaveActivity _laser;

    [SerializeField] private Collider2D _collider;

    private void Start()
    {
        _collider.enabled = true;

        _laser.Activate();
    }
}
