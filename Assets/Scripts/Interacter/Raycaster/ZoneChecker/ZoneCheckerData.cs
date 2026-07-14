using UnityEngine;

[CreateAssetMenu(fileName = nameof(ZoneCheckerData), menuName = nameof(ZoneCheckerData))]
public class ZoneCheckerData : ScriptableObject
{
    [SerializeField] private float _radius;

    public float Radius => _radius;
}
