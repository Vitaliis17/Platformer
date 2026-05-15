using System;

public interface IContainerReceiver<T> where T : Enum
{
    T Get(int index);
}