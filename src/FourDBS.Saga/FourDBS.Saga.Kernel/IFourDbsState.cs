namespace FourDBS.Saga.Kernel;

public interface IFourDbsEvent
{
    IFourDbsEvent Parent { get; set; }

    void ChangeEvent(string EventName);

    void PushEvent(string EventName);

    void PopEvent();

    void Update(float deltaTime);

    void Enter();

    void Exit();

    void TriggerEvent(string name);

    void TriggerEvent(string name, EventArgs eventArgs);
}