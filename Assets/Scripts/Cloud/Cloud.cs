using UnityEngine;
using R3;
using Zenject;

public class Cloud : MonoBehaviour, IHavePositionEvent, ISpriteSetter
{
    [Inject] private ISpeedRandomizer _speedRandomizer;

    private readonly Subject<Vector2> _positionChanged = new();

    private SpriteRenderer _spriteRenderer;
    private float _speed;

    public Observable<Vector2> PositionChanged => _positionChanged;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _speed = _speedRandomizer.GetRandomSpeed();
    }

    private void FixedUpdate()
    {
        transform.Translate(transform.right * _speed * Time.fixedDeltaTime);

        _positionChanged.OnNext(transform.position);
    }

    private void OnDestroy()
        => _positionChanged?.Dispose();

    public void SetSprite(Sprite sprite)
        => _spriteRenderer.sprite = sprite;
}
