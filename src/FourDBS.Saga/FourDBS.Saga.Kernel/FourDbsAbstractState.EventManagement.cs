namespace FourDBS.Saga.Kernel;

public abstract partial class FourDbsAbstractEvent
{
    public void TriggerEvent(string name)
    {
        TriggerEvent(name: name, eventArgs: EventArgs.Empty);
    }

    public void TriggerEvent(string name, EventArgs eventArgs)
    {
        if (_activeChildren.Count > 0)
        {
            _activeChildren.Peek().TriggerEvent(name: name, eventArgs: eventArgs);
            return;
        }

        if (_events.TryGetValue(key: name, value: out var myEvent))
        {
            myEvent(obj: eventArgs);
        }
    }

    public void SetEvent(string identifier, Action<EventArgs> eventTriggeredAction)
    {
        SetEvent<EventArgs>(identifier: identifier, eventTriggeredAction: eventTriggeredAction);
    }

    public void SetEvent<TEvent>(string identifier, Action<TEvent> eventTriggeredAction) where TEvent : EventArgs
    {
        _events.Add(key: identifier, value: args => eventTriggeredAction(obj: CheckEventArgs<TEvent>(identifier: identifier, args: args)));
    }

    private static TEvent CheckEventArgs<TEvent>(string identifier, EventArgs args) where TEvent : EventArgs
    {
        try
        {
            return (TEvent)args;
        }
        catch (InvalidCastException ex)
        {
            throw new ApplicationException(message: $@"No event '{identifier}' could be invoked with an argument of type {args.GetType().Name} as expected", innerException: ex);
        }
    }
}