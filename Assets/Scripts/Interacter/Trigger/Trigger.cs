using UnityEngine;
using R3;

public class Trigger : MonoBehaviour, IHaveTriggerEvent
{
    private readonly ReactiveProperty<bool> _isTriggered = new();

    public ReadOnlyReactiveProperty<bool> IsTriggered => _isTriggered;

    private void Start()
        => GetComponent<Collider2D>().isTrigger = true;

    protected virtual void OnTriggerEnter2D(Collider2D collision)
        => _isTriggered.OnNext(true);

    protected virtual void OnTriggerExit2D(Collider2D collision)
        => _isTriggered.OnNext(false);

    private void OnDestroy()
        => _isTriggered?.Dispose();
}
