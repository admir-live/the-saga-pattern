namespace FourDBS.Saga.Kernel;

public abstract partial class FourDbsAbstractEvent
{
    public void SetEnterAction(Action onEnter)
    {
        _onEnter = onEnter;
    }

    public void SetExitAction(Action onExit)
    {
        _onExit = onExit;
    }

    public void SetUpdateAction(Action<float> onUpdate)
    {
        _onUpdate = onUpdate;
    }


    private void Execute()
    {
        for (var i = 0; i < _conditions.Count; i++)
        {
            if (_conditions[index: i].Predicate())
            {
                _conditions[index: i].Action();
            }
        }
    }
}