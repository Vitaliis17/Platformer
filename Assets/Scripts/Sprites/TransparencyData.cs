using UnityEngine;

[CreateAssetMenu(fileName = nameof(TransparencyData), menuName = nameof(TransparencyData))]
public class TransparencyData : ScriptableObject, ITransparencyData
{
    [SerializeField, Min(0)] private float _maxValue;
    [SerializeField, Min(0)] private float _minValue;

    private void OnValidate()
    {
        if(_maxValue < _minValue)
            _maxValue = _minValue;
    }

    public float MaxValue => _maxValue;
    public float MinValue => _minValue;
}
