using FourDBS.Saga.Database;
using FourDBS.Saga.Database.Domain;
using FourDBS.Saga.Example.Ship;
using FourDBS.Saga.Kernel;

namespace FourDBS.Saga.Example.Payment;

public class MakePaymentEvent : FourDbsAbstractEvent
{
    public MakePaymentEvent(Product product, Account account)
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
        var account = Static.Database.Accounts.FirstOrDefault(predicate: account => account.Number == Account.Number);
        if (account is null)
        {
            Console.WriteLine(value: "The account does not exists.");
            return false;
        }

        return Product.Price.Value <= account.Balance.Value;
    }

    private void OnEnter()
    {
        Console.WriteLine(value: $"Event: {nameof(MakePaymentEvent)}: Making payment for product: {Product}");
        AddChild(newEvent: new MakeShipEvent(product: Product, account: Account));
        ChangeEvent(EventName: nameof(MakeShipEvent));
    }
}