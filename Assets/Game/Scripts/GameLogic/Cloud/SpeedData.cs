using UnityEngine;

[CreateAssetMenu(fileName = nameof(SpeedData), menuName = nameof(SpeedData))]
public class SpeedData : ScriptableObject, ISpeedRandomizer
{
    [SerializeField, Min(0)] private float _minSpeed;
    [SerializeField] private float _maxSpeed;

    private void OnValidate()
    {
        if(_maxSpeed < _minSpeed)
            _maxSpeed = _minSpeed;
    }

    public float GetRandomSpeed()
        => Random.Range(_minSpeed, _maxSpeed);
}