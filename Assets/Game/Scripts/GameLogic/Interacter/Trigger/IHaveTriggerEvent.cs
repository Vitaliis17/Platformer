using R3;

public interface IHaveTriggerEvent
{
    ReadOnlyReactiveProperty<bool> IsTriggered { get; }

    public bool HaveTriggered();
}