using UnityEngine;

[CreateAssetMenu(fileName = "ZoneCheckerData", menuName = "ZoneChecker")]
public class ZoneCheckerData : ScriptableObject
{
    [SerializeField] private float _radius;

    public float Radius => _radius;
}
