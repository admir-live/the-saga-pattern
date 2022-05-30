using FourDBS.Saga.Database.Domain;
using FourDBS.Saga.Kernel;

namespace FourDBS.Saga.Example.Order;

public class RollbackOrderEvent : FourDbsAbstractEvent
{
    public RollbackOrderEvent(Product product, Account account)
    {
        Product = product;
        Account = account;
    }

    public Product Product { get; set; }
    public Account Account { get; set; }
}