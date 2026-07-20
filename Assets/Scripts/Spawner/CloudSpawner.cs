using UnityEngine;
using Zenject;

public class CloudSpawner : Spawner<Cloud>
{
    private ISpriteRandomizer _spriteRandomizer;

    [Inject]
    public CloudSpawner(ISpriteRandomizer spriteRandomizer, Cloud prefab, Transform container) : base(prefab, container)
        => _spriteRandomizer = spriteRandomizer;

    public override Cloud GetElement()
    {
        Cloud cloud = base.GetElement();

        Sprite sprite = _spriteRandomizer.GetRandomSprite();

        cloud.SetSprite(sprite);
        SetRandomScale(cloud);

        return cloud;
    }

    private void SetRandomScale(Cloud cloud)
    {
        const float BaseScale = 1f;

        float randomScaleX = _spriteRandomizer.GetRandomScaleX();
        cloud.transform.localScale = new(randomScaleX, BaseScale);
    }
}