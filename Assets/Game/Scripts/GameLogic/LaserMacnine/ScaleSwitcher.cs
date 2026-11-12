using Zenject;
using UnityEngine;

public class ScaleSwitcher : IHaveActivity
{
    private readonly Transform _scaler;

    private readonly Vector2 _minScale;
    private readonly Vector2 _maxScale;

    [Inject]
    public ScaleSwitcher(Transform transform, IRaycastPointer pointer, float minScale = 0)
    {
        _scaler = transform;

        Vector2 point = pointer.Raycast(_scaler.position, _scaler.up);

        _maxScale = new Vector2(_scaler.localScale.x, Vector2.Distance(point, _scaler.position));
        _minScale = new Vector2(_scaler.localScale.x, minScale);
    }

    public void Activate()
        => _scaler.localScale = _maxScale;

    public void Deactivate()
        => _scaler.localScale = _minScale;
}