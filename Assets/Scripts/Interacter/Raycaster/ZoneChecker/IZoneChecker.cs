using UnityEngine;

public interface IZoneChecker
{
    bool IsInside(Vector2 origin, Vector2 target);
}
