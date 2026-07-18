using UnityEngine;
using Zenject;

public class ZoneChecker : IZoneChecker
{
    private readonly ZoneCheckerData _data;

    [Inject]
    public ZoneChecker(ZoneCheckerData zoneCheckerData) 
        => _data = zoneCheckerData;

    public bool IsInside(Vector2 origin, Vector2 target)
    {
        Vector2 delta = origin - target;
        
        return delta.magnitude <= _data.Radius;
    }
}