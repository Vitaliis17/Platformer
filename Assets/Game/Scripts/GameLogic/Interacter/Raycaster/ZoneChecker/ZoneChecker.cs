using UnityEngine;
using Zenject;

public class ZoneChecker : IZoneChecker
{
    private readonly float _radius;

    [Inject]
    public ZoneChecker(Transform circleBorder)
        => _radius = circleBorder.localScale.x;

    public bool IsInside(Vector2 origin, Vector2 target)
    { 
        Vector2 delta = origin - target;
        
        return delta.magnitude <= _radius;
    }

    public Vector2 ReadPoint(Vector2 origin, Vector2 target)
        => Vector2.MoveTowards(origin, target, _radius);
}