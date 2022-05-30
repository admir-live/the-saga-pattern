namespace FourDBS.Saga.Kernel;

public class FourDbsEventMachineFactory
{
    private readonly FourDbsGeneralEvent _rootEvent;

    public FourDbsEventMachineFactory()
    {
        _rootEvent = new FourDbsGeneralEvent();
    }

    public IFourDbsEventBuilder<T, FourDbsEventMachineFactory> Event<T>() where T : FourDbsAbstractEvent, new()
    {
        return new FourDbsEventBuilder<T, FourDbsEventMachineFactory>(parentBuilder: this, parentEvent: _rootEvent);
    }

    public IFourDbsEventBuilder<T, FourDbsEventMachineFactory> Event<T>(string EventName) where T : FourDbsAbstractEvent, new()
    {
        return new FourDbsEventBuilder<T, FourDbsEventMachineFactory>(parentBuilder: this, parentEvent: _rootEvent, name: EventName);
    }

    public IFourDbsEventBuilder<FourDbsGeneralEvent, FourDbsEventMachineFactory> Event(string EventName)
    {
        return new FourDbsEventBuilder<FourDbsGeneralEvent, FourDbsEventMachineFactory>(parentBuilder: this, parentEvent: _rootEvent, name: EventName);
    }

    public IFourDbsEvent Build()
    {
        return _rootEvent;
    }
}