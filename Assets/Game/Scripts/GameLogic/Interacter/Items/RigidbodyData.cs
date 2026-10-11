using UnityEngine;

[CreateAssetMenu(fileName = nameof(RigidbodyData), menuName = nameof(RigidbodyData))]
public class RigidbodyData : ScriptableObject, IRigidbodyData
{
    [SerializeField] private float _baseGravity;
    [SerializeField] private float _minGravity;

    [SerializeField] private float _minMass;
    [SerializeField] private float _baseMass;

    public float BaseGravity => _baseGravity;
    public float MinGravity => _minGravity;

    public float MinMass => _minMass;
    public float BaseMass => _baseMass;
}