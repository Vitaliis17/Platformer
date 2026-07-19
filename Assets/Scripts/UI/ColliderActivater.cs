using R3;
using UnityEngine;
using Zenject;

public class ColliderActivater : MonoBehaviour
{
    [Inject] private IMovementReader _reader;
    
    [SerializeField] private Collider2D _collider;

    private void Start()
    {
        _reader.Moved
            .Select(direction => direction == Vector2.zero)
            .Subscribe(isStoping => _collider.enabled = isStoping)
            .AddTo(_collider.gameObject);
    }
}
