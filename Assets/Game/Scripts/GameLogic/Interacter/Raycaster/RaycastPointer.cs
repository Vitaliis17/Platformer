using UnityEngine;

public class RaycastPointer : IRaycastPointer
{
    public Vector2 Raycast(Vector2 current, Vector2 direction)
    {
        const int MaxDistance = 100;

        RaycastHit2D hit = Physics2D.Raycast(current, direction);

        if (hit.collider == null)
            return direction * MaxDistance;

        return hit.point;
    }
}