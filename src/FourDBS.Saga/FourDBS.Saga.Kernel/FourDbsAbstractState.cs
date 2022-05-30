namespace FourDBS.Saga.Kernel;

public abstract partial class FourDbsAbstractEvent : IFourDbsEvent
{
    private readonly Stack<IFourDbsEvent> _activeChildren = new();
    private readonly IDictionary<string, IFourDbsEvent> _children = new Dictionary<string, IFourDbsEvent>();
    private readonly IList<Operation> _conditions = new List<Operation>();
    private readonly IDictionary<string, Action<EventArgs>> _events = new Dictionary<string, Action<EventArgs>>();

    private Action _onEnter;
    private Action _onExit;
    private Action<float> _onUpdate;

    public IFourDbsEvent Parent { get; set; }

    private static void ThrowException(string EventName)
    {
        throw new ApplicationException(message: $@"We are attempted to change the Event to '{EventName}', but it is not listed as a child.");
    }
}