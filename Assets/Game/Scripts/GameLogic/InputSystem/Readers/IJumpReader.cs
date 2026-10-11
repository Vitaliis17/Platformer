using R3;

public interface IJumpReader
{
    public Observable<bool> Jumped { get; }
}
