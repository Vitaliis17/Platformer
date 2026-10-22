using R3;

public interface IHaveAuthenticateEvent
{
    Observable<string> Authenticated { get; }
}