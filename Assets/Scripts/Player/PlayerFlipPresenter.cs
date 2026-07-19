using R3;
using UnityEngine;
using Zenject;

public class PlayerFlipPresenter : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _playerRenderer;

    [Inject] private IMovementReader _reader;
    [Inject] private IFlipper _flipper;

    private float _currentSign = 1;

    private void Start()
    {
        Observable<Vector2> observable = _reader.Moved.Where(direction => direction.sqrMagnitude != 0f)
            .Where(direction => Mathf.Sign(direction.x) != Mathf.Sign(_currentSign));

        observable.Subscribe(_ => _flipper.FlipX(_playerRenderer)).AddTo(this);
        observable.Subscribe(direction => _currentSign = direction.x).AddTo(this);
    }
}
