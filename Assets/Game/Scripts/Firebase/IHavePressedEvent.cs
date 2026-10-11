using R3;

public interface IHavePressedEvent
{
    Observable<(string, string)> Pressed { get; }
}