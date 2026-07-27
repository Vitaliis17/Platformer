using System;

public interface IContainer : IEventContainerSetter, IContainerReceiver, IEmptyChecker, IEventEmptySetter, IDisposable
{
}
