using UnityEngine;
using Zenject;

public class TransparencySwitcher : MonoBehaviour, ITransparencySwitcher
{
    [Inject] private ITransparencyData _data;

    [SerializeField] private SpriteRenderer _spriteRenderer;

    public void Activate()
    {
        Color oldColor = _spriteRenderer.color;
        oldColor.a = _data.MaxValue;
        _spriteRenderer.color = oldColor;
    }

    public void Deactivate()
    {
        Color oldColor = _spriteRenderer.color;
        oldColor.a = _data.MinValue;
        _spriteRenderer.color = oldColor;
    }
}