using R3;

public interface IHaveOnClickEvent
{
    Observable<Unit> Clicked { get; }
}