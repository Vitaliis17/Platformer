using UnityEngine;
using Zenject;
using R3;
using System;

public class CloudSpawnerPresenter : MonoBehaviour
{
    [Inject] private ISpawner<Cloud> _spawner;

    [SerializeField] private float _minPositionY;
    [SerializeField] private float _maxPositionY;

    [SerializeField] private float _positionOffset;

    [SerializeField] private float _spawnPositionX;
    [SerializeField] private float _releasingPositionX;

    private float _currentRandomNumber = 0;
    private float _lastRandomNumber = float.MinValue;

    private void Start()
    {
        Observable
            .Interval(TimeSpan.FromSeconds(80f))
            .Subscribe(_ => Spawn())
            .AddTo(this);
    }

    private void Spawn()
    {
        Cloud cloud = _spawner.GetElement();

        do
            _currentRandomNumber = UnityEngine.Random.Range(_minPositionY, _maxPositionY);
        while (IsInRange());

        _lastRandomNumber = _currentRandomNumber;

        cloud.transform.position = new(_spawnPositionX, _currentRandomNumber);

        cloud.PositionChanged
            .Where(position => position.x > _releasingPositionX)
            .Take(1)
            .Subscribe(_ => _spawner.ReleaseElement(cloud))
            .AddTo(cloud);
    }

    private bool IsInRange()
    {
        bool isInUpperLimit = _currentRandomNumber > _lastRandomNumber && _currentRandomNumber - _positionOffset < _lastRandomNumber;
        bool isInLowerLimit = _currentRandomNumber < _lastRandomNumber && _currentRandomNumber + _positionOffset > _lastRandomNumber;

        return isInUpperLimit || isInLowerLimit;
    }
}