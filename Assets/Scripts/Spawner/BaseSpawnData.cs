using UnityEngine;

[CreateAssetMenu(fileName = nameof(BaseSpawnData), menuName = nameof(BaseSpawnData))]
public class BaseSpawnData : ScriptableObject, IBaseSpawnData
{
    [SerializeField, Min(0)] private float _spawnTime;

    public float SpawnTime => _spawnTime;
}