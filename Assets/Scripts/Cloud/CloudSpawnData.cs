using UnityEngine;

[CreateAssetMenu(fileName = nameof(CloudSpawnData), menuName = nameof(CloudSpawnData))]
public class CloudSpawnData : ScriptableObject
{
    [SerializeField] private float _minPositionY;
    [SerializeField] private float _maxPositionY;

    [SerializeField] private float _positionOffset;

    [SerializeField] private float _spawnPositionX;
    [SerializeField] private float _releasingPositionX;

    public float RandomPositionY => GeneratePositionY();
    public float PositionOffset => _positionOffset;
    public float SpawnPositionX => _spawnPositionX;
    public float ReleasingPositionX => _releasingPositionX;

    private float GeneratePositionY()
        => Random.Range(_minPositionY, _maxPositionY);
}
