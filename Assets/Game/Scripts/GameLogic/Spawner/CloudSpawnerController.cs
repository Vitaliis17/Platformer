using Zenject;
using R3;
using System;

public class CloudSpawnerController : IInitializable, IDisposable
{
    [Inject] private ISpawner<Cloud> _spawner;
    [Inject] private ICloudSpawnData _spawnData;

    [Inject] private IBaseSpawnData _baseSpawnData;

    private float _currentRandomNumber = 0;
    private float _lastRandomNumber = float.MinValue;

    private IDisposable _disposable;

    public void Initialize()
    {
        _disposable = Observable
            .Interval(TimeSpan.FromSeconds(_baseSpawnData.SpawnTime))
            .Subscribe(_ => Spawn());
    }

    public void Dispose()
        => _disposable?.Dispose();

    private void Spawn()
    {
        Cloud cloud = _spawner.GetElement();
        
        do
            _currentRandomNumber = _spawnData.RandomPositionY;
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