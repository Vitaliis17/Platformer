using R3;

public interface IHaveLoginEvent
{
    Observable<Unit> IsLogin { get; }
}