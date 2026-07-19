using UnityEngine;
using R3;

public class PositionChecker : MonoBehaviour
{
    [SerializeField] private RectTransform _rectTransform;

    private readonly Subject<bool> _isMoved = new();

    public Observable<bool> IsMoved => _isMoved;
}