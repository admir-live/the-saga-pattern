using FourDBS.Saga.Database.Domain;
using FourDBS.Saga.Example.Order;
using FourDBS.Saga.Kernel;

namespace FourDBS.Saga.Example;

public class SimulateApiRequest : FourDbsAbstractEvent
{
    public SimulateApiRequest()
    {
        Product = new Product(Name: "DELL XPS", Price: new Money(Value: 1000));
        Account = new Account(Number: 1001, Balance: new Money(Value: 5000));

        SetEnterAction(onEnter: OnEnter);
        Enter();
        Update(deltaTime: DateTime.UtcNow.Ticks);
    }

    public Guid OrderId { get; set; }
    public Account Account { get; set; }
    public Product Product { get; set; }

    private void OnEnter()
    {
        OrderId = Guid.NewGuid();
        Console.WriteLine(value: $"Event: {nameof(SimulateApiRequest)}: has been taken for product: {Product}");
        AddChild(newEvent: new MakeOrderEvent(product: Product, account: Account));
        ChangeEvent(EventName: nameof(MakeOrderEvent));
    }
}