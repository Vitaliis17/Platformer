using System;

public interface IContainer<T> where T : Enum
{
    T Get(int index);
}