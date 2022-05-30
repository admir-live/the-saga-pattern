namespace FourDBS.Saga.Kernel;

public abstract partial class FourDbsAbstractEvent
{
    public void AddChild(IFourDbsEvent newEvent, string EventName)
    {
        try
        {
            _children.Add(key: EventName, value: newEvent);
            newEvent.Parent = this;
        }
        catch (ArgumentException)
        {
            throw new ApplicationException(message: $@"In the list of children, a Event with the name '{EventName}' already exists.");
        }
    }

    public void AddChild(IFourDbsEvent newEvent)
    {
        var name = newEvent.GetType().Name;
        AddChild(newEvent: newEvent, EventName: name);
    }

    public void SetCondition(Func<bool> predicate, Action action)
    {
        _conditions.Add(item: new Operation
        {
            Predicate = predicate,
            Action = action
        });
    }

    private struct Operation
    {
        public Func<bool> Predicate;
        public Action Action;
    }
}