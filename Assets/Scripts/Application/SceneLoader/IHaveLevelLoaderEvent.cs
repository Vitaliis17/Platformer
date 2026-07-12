using R3;

public interface IHaveLevelLoaderEvent
{
    Observable<int> LevelChanging { get; }
}