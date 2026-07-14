using UnityEngine;
using R3;

public class Cloud : MonoBehaviour, IHavePositionEvent
{
    [SerializeField] private SpeedData _data;

    private readonly Subject<Vector2> _positionChanged = new();

    private float _speed;

    public Observable<Vector2> PositionChanged => _positionChanged;
    public SpriteRenderer SpriteRenderer { get; private set; }

    private void Start()
    {
        _speed = _data.GetRandomSpeed();
        SpriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void FixedUpdate()
    {
        transform.Translate(transform.right * _speed * Time.fixedDeltaTime);

        _positionChanged.OnNext(transform.position);
    }

    private void OnDestroy()
        => _positionChanged?.Dispose();
}
