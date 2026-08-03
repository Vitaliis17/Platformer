using UnityEngine;

[CreateAssetMenu(fileName = nameof(VelocityData), menuName = nameof(VelocityData))]
public class VelocityData : ScriptableObject, IHaveMultiplier
{
    [SerializeField] private float _multiplier;

    public float Multiplier => _multiplier;
}