using R3;

internal interface ISaved
{
    Observable<Unit> Saved { get; }
}