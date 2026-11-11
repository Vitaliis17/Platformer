using R3;

public interface IHaveSaveEvent
{
    Observable<SaveData> SaveChanged { get; }
}