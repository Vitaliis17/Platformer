using R3;

public interface ITrigger
{
    Observable<bool> IsTriggered { get; }
}