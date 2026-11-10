using UnityEngine;

public interface IZoneChecker
{
    bool IsInside(Vector2 origin, Vector2 target);

    Vector2 ReadPoint(Vector2 origin, Vector2 target);
}
