using UnityEngine;
using R3;

public abstract class Trigger : MonoBehaviour
{
    private readonly Subject<bool> _isTriggered = new();

    public Observable<bool> IsTriggered => _isTriggered;

    private void Start()
        => GetComponent<Collider2D>().isTrigger = true;

    private void OnTriggerEnter2D(Collider2D collision)
        => _isTriggered.OnNext(true);

    private void OnTriggerExit2D(Collider2D collision)
        => _isTriggered.OnNext(false);

    private void OnDestroy()
        => _isTriggered?.Dispose();
}
