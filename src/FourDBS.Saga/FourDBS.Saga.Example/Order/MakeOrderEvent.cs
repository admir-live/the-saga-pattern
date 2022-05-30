using FourDBS.Saga.Database;
using FourDBS.Saga.Database.Domain;
using FourDBS.Saga.Example.Payment;
using FourDBS.Saga.Kernel;

namespace FourDBS.Saga.Example.Order;

public class MakeOrderEvent : FourDbsAbstractEvent
{
    public MakeOrderEvent(Product product, Account account)
    {
        Product = product;
        Account = account;
        SetCondition(predicate: CheckStatus, action: () => SetEnterAction(onEnter: OnEnter));
        Update(deltaTime: DateTime.UtcNow.Ticks);
    }

    public Product Product { get; set; }
    public Account Account { get; set; }

    private void OnEnter()
    {
        Console.WriteLine(value: $"Event: {nameof(MakeOrderEvent)}: Making order for product: {Product}");
        AddChild(newEvent: new MakePaymentEvent(product: Product, account: Account));
        ChangeEvent(EventName: nameof(MakePaymentEvent));
    }

    private bool CheckStatus()
    {
        return Static.Database.Products.Any(predicate: product => product == Product);
    }
}