namespace FourDBS.Saga.Kernel;

public interface IFourDbsEventBuilder<out T, out TParent> where T : FourDbsAbstractEvent, new()
{
    IFourDbsEventBuilder<TFourDbsEvent, IFourDbsEventBuilder<T, TParent>> Event<TFourDbsEvent>() where TFourDbsEvent : FourDbsAbstractEvent, new();

    IFourDbsEventBuilder<TFourDbsEvent, IFourDbsEventBuilder<T, TParent>> Event<TFourDbsEvent>(string name) where TFourDbsEvent : FourDbsAbstractEvent, new();

    IFourDbsEventBuilder<FourDbsGeneralEvent, IFourDbsEventBuilder<T, TParent>> Event(string name);

    IFourDbsEventBuilder<T, TParent> Enter(Action<T> onEnter);

    IFourDbsEventBuilder<T, TParent> Exit(Action<T> onExit);

    IFourDbsEventBuilder<T, TParent> Update(Action<T, float> onUpdate);

    IFourDbsEventBuilder<T, TParent> Condition(Func<bool> predicate, Action<T> action);

    IFourDbsEventBuilder<T, TParent> Event(string identifier, Action<T> action);

    IFourDbsEventBuilder<T, TParent> Event<TEvent>(string identifier, Action<T, TEvent> action) where TEvent : EventArgs;

    TParent End();
}