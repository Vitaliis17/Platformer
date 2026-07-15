using UnityEngine;

[CreateAssetMenu(fileName = nameof(SpriteData), menuName = nameof(SpriteData))]
public class SpriteData : ScriptableObject
{
    [SerializeField, Min(0)] private float _minScaleX;
    [SerializeField] private float _maxScaleX;

    [SerializeField] private Sprite[] _sprites;

    private void OnValidate()
    {
        if(_maxScaleX < _minScaleX)
            _maxScaleX = _minScaleX;
    }

    public float GetRandomScaleX()
        => Random.Range(_minScaleX, _maxScaleX);

    public Sprite GetRandomSprite()
    {
        const int MinIndex = 0;

        int randomIndex = Random.Range(MinIndex, _sprites.Length - 1);

        return _sprites[randomIndex];
    }
}