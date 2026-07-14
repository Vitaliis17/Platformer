using UnityEngine;
using Zenject;

public class CloudSpawner : Spawner<Cloud>
{
    private SpriteData _data;

    [Inject]
    public CloudSpawner(SpriteData data, Cloud prefab, Transform container) : base(prefab, container)
        => _data = data;

    public override Cloud GetElement()
    {
        Cloud cloud = base.GetElement();

        cloud.SpriteRenderer.sprite = _data.GetRandomSprite();
        SetRandomScale(cloud);

        return cloud;
    }

    private void SetRandomScale(Cloud cloud)
    {
        const float BaseScale = 1f;

        float randomScaleX = _data.GetRandomScaleX();
        cloud.transform.localScale = new(randomScaleX, BaseScale);
    }
}