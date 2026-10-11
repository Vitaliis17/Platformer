using UnityEngine;

[CreateAssetMenu(fileName = nameof(PauseData), menuName = nameof(PauseData))]
public class PauseData : ScriptableObject, IGameSpeedSender
{
    [SerializeField, Min(0)] private float _minGameSpeed;
    [SerializeField, Min(0)] private float _baseGameSpeed;

    public float MinGameSpeed => _minGameSpeed;
    public float BaseGameSpeed => _baseGameSpeed;
}