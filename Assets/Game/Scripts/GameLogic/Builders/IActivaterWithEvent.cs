using R3;

internal interface IActivaterWithEvent
{
    Observable<Unit> Activated { get; }

    void Activate();
}