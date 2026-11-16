using Zenject;
using UnityEngine;
using DG.Tweening;

public class ScaleSwitcher : IHaveActivity
{
    private readonly Transform _scaler;

    private readonly Vector2 _minScale;
    private readonly Vector2 _maxScale;

    private float _time;

    [Inject]
    public ScaleSwitcher(Transform transform, IRaycastPointer pointer, float minScale = 0)
    {
        _scaler = transform;

        Vector2 point = pointer.Raycast(_scaler.position, _scaler.up);

        _maxScale = new Vector2(_scaler.localScale.x, Vector2.Distance(point, _scaler.position));
        _minScale = new Vector2(_scaler.localScale.x, minScale);
    }

    public void Activate()
        => _scaler.DOScale(_maxScale, _time);

    public void Deactivate()
        => _scaler.DOScale(_minScale, _time);
}