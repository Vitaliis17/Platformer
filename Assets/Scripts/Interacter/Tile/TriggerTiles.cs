using UnityEngine;
using R3;
using UnityEngine.Tilemaps;

[RequireComponent(typeof(TilemapCollider2D))]
public class TriggerTiles : MonoBehaviour
{
    private readonly Subject<bool> _isTriggered = new();

    public Observable<bool> IsTriggered => _isTriggered;

    private void OnTriggerEnter2D(Collider2D collision)
        => _isTriggered.OnNext(true);

    private void OnTriggerExit2D(Collider2D collision)
        => _isTriggered.OnNext(false);

    private void OnDestroy()
        => _isTriggered?.Dispose();
}
