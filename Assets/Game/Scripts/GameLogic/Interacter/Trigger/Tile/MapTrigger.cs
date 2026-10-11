using UnityEngine;
using UnityEngine.Tilemaps;

[RequireComponent(typeof(TilemapCollider2D))]
public class MapTrigger : Trigger
{
    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);

        if (collision.TryGetComponent(out IGravitySwitcher switcher))
            switcher.TurnOffGravity();
    }

    protected override void OnTriggerExit2D(Collider2D collision)
    {
        base.OnTriggerExit2D(collision);

        if (collision.TryGetComponent(out IGravitySwitcher switcher))
            switcher.TurnOnGravity();
    }
}
