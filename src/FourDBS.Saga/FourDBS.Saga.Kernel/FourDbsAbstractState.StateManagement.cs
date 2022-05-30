namespace FourDBS.Saga.Kernel;

public abstract partial class FourDbsAbstractEvent
{
    public void ChangeEvent(string EventName)
    {
        if (!_children.TryGetValue(key: EventName, value: out var newEvent))
        {
            ThrowException(EventName: EventName);
        }

        if (_activeChildren.Count > 0)
        {
            _activeChildren.Pop().Exit();
        }

        _activeChildren.Push(item: newEvent);
        newEvent!.Enter();
    }

    public void PushEvent(string EventName)
    {
        if (!_children.TryGetValue(key: EventName, value: out var newEvent))
        {
            ThrowException(EventName: EventName);
        }

        _activeChildren.Push(item: newEvent);
        newEvent!.Enter();
    }

    public void PopEvent()
    {
        switch (_activeChildren.Count)
        {
            case > 0:
                _activeChildren.Pop().Exit();
                break;
            default:
                throw new ApplicationException(message: "PopEvent called on Event with no active children to pop.");
        }
    }

    public void Update(float deltaTime)
    {
        if (_activeChildren.Count > 0)
        {
            _activeChildren.Peek().Update(deltaTime: deltaTime);
            return;
        }

        _onUpdate?.Invoke(obj: deltaTime);
        Execute();
    }

    public void Enter()
    {
        _onEnter?.Invoke();
    }

    public void Exit()
    {
        _onExit?.Invoke();

        while (_activeChildren.Count > 0)
        {
            _activeChildren.Pop().Exit();
        }
    }
}