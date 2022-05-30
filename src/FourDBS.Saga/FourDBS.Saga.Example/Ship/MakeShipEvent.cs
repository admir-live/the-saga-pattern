using FourDBS.Saga.Database.Domain;
using FourDBS.Saga.Kernel;

namespace FourDBS.Saga.Example.Ship;

public class MakeShipEvent : FourDbsAbstractEvent
{
    public MakeShipEvent(Product product, Account account)
    {
        Product = product;
        Account = account;
        SetCondition(predicate: CheckStatus, action: () => SetEnterAction(onEnter: OnEnter));
        Update(deltaTime: DateTime.UtcNow.Ticks);
    }

    public Product Product { get; set; }
    public Account Account { get; set; }

    private bool CheckStatus()
    {
        return true;
    }

    private void OnEnter()
    {
        Console.WriteLine(value: $"Event: {nameof(MakeShipEvent)}: Shipping for product: {Product}");
    }
}