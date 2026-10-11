using UnityEngine;
using R3;

public class Trigger : MonoBehaviour, IHaveTriggerEvent
{
    private readonly ReactiveProperty<bool> _isTriggered = new();

    private int _currentTriggered = 0;

    public ReadOnlyReactiveProperty<bool> IsTriggered => _isTriggered;

    private void Start()
        => GetComponent<Collider2D>().isTrigger = true;

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        _currentTriggered++;

        _isTriggered.OnNext(true);
    }

    protected virtual void OnTriggerExit2D(Collider2D collision)
    {
        _currentTriggered--;

        _isTriggered.OnNext(false);
    }

    private void OnDestroy()
        => _isTriggered?.Dispose();

    public bool HaveTriggered()
    {
        const int NoEntered = 0;

        return _currentTriggered > NoEntered;
    }
}
