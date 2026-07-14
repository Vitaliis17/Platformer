using UnityEngine;
using Zenject;
using R3;
using System;

public class CloudSpawnerPresenter : MonoBehaviour
{
    [Inject] private ISpawner<Cloud> _spawner;

    [SerializeField] private CloudSpawnData _spawnData;

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
            _currentRandomNumber = _spawnData.SpawnPositionX;
        while (IsInRange());

        _lastRandomNumber = _currentRandomNumber;

        cloud.transform.position = new(_spawnData.SpawnPositionX, _currentRandomNumber);

        cloud.PositionChanged
            .Where(position => position.x > _spawnData.ReleasingPositionX)
            .Take(1)
            .Subscribe(_ => _spawner.ReleaseElement(cloud))
            .AddTo(cloud);
    }

    private bool IsInRange()
    {
        bool isInUpperLimit = _currentRandomNumber > _lastRandomNumber && _currentRandomNumber - _spawnData.PositionOffset < _lastRandomNumber;
        bool isInLowerLimit = _currentRandomNumber < _lastRandomNumber && _currentRandomNumber + _spawnData.PositionOffset > _lastRandomNumber;

        return isInUpperLimit || isInLowerLimit;
    }
}