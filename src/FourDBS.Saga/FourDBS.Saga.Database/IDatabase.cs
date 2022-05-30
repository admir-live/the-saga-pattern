using FourDBS.Saga.Database.Domain;

namespace FourDBS.Saga.Database;

public interface IDatabase
{
    IEnumerable<Product> Products { get; }
    IEnumerable<Account> Accounts { get; }
}