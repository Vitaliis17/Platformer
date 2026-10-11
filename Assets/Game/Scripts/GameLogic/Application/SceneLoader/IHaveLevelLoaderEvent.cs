using R3;
using System;

public interface IHaveLevelLoaderEvent : IDisposable
{
    Observable<int> LevelChanging { get; }
}