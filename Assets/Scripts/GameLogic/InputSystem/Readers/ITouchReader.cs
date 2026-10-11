using R3;

public interface ITouchReader
{
    public Observable<bool> PressChanged { get; }
}
