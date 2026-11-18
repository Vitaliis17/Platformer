using Zenject;
using UnityEngine;
using DG.Tweening;

public class ScaleSwitcher : IHaveActivity
{
    private readonly Transform _scaler;

    private readonly Vector2 _minScale;
    private readonly Vector2 _maxScale;

    private IScaleSwitcherData _data;

    [Inject]
    public ScaleSwitcher(Transform transform, IScaleSwitcherData data, IRaycastPointer pointer, float minScale = 0)
    {
        _scaler = transform;
        _data = data;

        Vector2 point = pointer.Raycast(_scaler.position, _scaler.up);

        _maxScale = new Vector2(_scaler.localScale.x, Vector2.Distance(point, _scaler.position));
        _minScale = new Vector2(_scaler.localScale.x, minScale);
    }

    public void Activate()
        => _scaler.DOScale(_maxScale, _data.AnimationTime);

    public void Deactivate()
        => _scaler.DOScale(_minScale, _data.AnimationTime);
}