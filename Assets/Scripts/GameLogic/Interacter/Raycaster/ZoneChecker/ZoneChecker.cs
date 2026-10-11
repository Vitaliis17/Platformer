using UnityEngine;
using Zenject;

public class ZoneChecker : IZoneChecker
{
    private readonly IHaveRadius _radiusHaver;

    [Inject]
    public ZoneChecker(IHaveRadius radiusHaver) 
        => _radiusHaver = radiusHaver;

    public bool IsInside(Vector2 origin, Vector2 target)
    {
        Vector2 delta = origin - target;
        
        return delta.magnitude <= _radiusHaver.Radius;
    }
}