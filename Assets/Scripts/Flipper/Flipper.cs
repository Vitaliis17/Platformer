using UnityEngine;

public class Flipper : IFlipper
{
    public void FlipX(SpriteRenderer sprite)
        => sprite.flipX = sprite.flipX == false;
}
