namespace FourDBS.Saga.Kernel;

public class FourDbsEventBuilder<T, TParent> : IFourDbsEventBuilder<T, TParent> where T : FourDbsAbstractEvent, new()
{
    private readonly T _Event;
    private readonly TParent _parentBuilder;

    public FourDbsEventBuilder(TParent parentBuilder, FourDbsAbstractEvent parentEvent)
    {
        _parentBuilder = parentBuilder;

        _Event = new T();
        parentEvent.AddChild(newEvent: _Event);
    }

    public FourDbsEventBuilder(TParent parentBuilder, FourDbsAbstractEvent parentEvent, string name)
    {
        _parentBuilder = parentBuilder;

        _Event = new T();
        parentEvent.AddChild(newEvent: _Event, EventName: name);
    }

    public IFourDbsEventBuilder<TNewEvent, IFourDbsEventBuilder<T, TParent>> Event<TNewEvent>() where TNewEvent : FourDbsAbstractEvent, new()
    {
        return new FourDbsEventBuilder<TNewEvent, IFourDbsEventBuilder<T, TParent>>(parentBuilder: this, parentEvent: _Event);
    }

    public IFourDbsEventBuilder<TNewEvent, IFourDbsEventBuilder<T, TParent>> Event<TNewEvent>(string name) where TNewEvent : FourDbsAbstractEvent, new()
    {
        return new FourDbsEventBuilder<TNewEvent, IFourDbsEventBuilder<T, TParent>>(parentBuilder: this, parentEvent: _Event, name: name);
    }

    public IFourDbsEventBuilder<FourDbsGeneralEvent, IFourDbsEventBuilder<T, TParent>> Event(string name)
    {
        return new FourDbsEventBuilder<FourDbsGeneralEvent, IFourDbsEventBuilder<T, TParent>>(parentBuilder: this, parentEvent: _Event, name: name);
    }

    public IFourDbsEventBuilder<T, TParent> Enter(Action<T> onEnter)
    {
        _Event.SetEnterAction(onEnter: () => onEnter(obj: _Event));
        return this;
    }

    public IFourDbsEventBuilder<T, TParent> Exit(Action<T> onExit)
    {
        _Event.SetExitAction(onExit: () => onExit(obj: _Event));
        return this;
    }

    public IFourDbsEventBuilder<T, TParent> Update(Action<T, float> onUpdate)
    {
        _Event.SetUpdateAction(onUpdate: dt => onUpdate(arg1: _Event, arg2: dt));
        return this;
    }

    public IFourDbsEventBuilder<T, TParent> Condition(Func<bool> predicate, Action<T> action)
    {
        _Event.SetCondition(predicate: predicate, action: () => action(obj: _Event));
        return this;
    }

    public IFourDbsEventBuilder<T, TParent> Event(string identifier, Action<T> action)
    {
        _Event.SetEvent<EventArgs>(identifier: identifier, eventTriggeredAction: _ => action(obj: _Event));
        return this;
    }

    public IFourDbsEventBuilder<T, TParent> Event<TEvent>(string identifier, Action<T, TEvent> action) where TEvent : EventArgs
    {
        _Event.SetEvent<TEvent>(identifier: identifier, eventTriggeredAction: args => action(arg1: _Event, arg2: args));
        return this;
    }

    public TParent End()
    {
        return _parentBuilder;
    }
}