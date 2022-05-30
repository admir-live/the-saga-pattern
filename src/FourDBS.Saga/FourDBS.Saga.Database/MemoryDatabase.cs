using FourDBS.Saga.Database.Domain;

namespace FourDBS.Saga.Database;

public class MemoryDatabase : IDatabase
{
    public MemoryDatabase()
    {
        Products = new List<Product>
        {
            new(Name: "DELL XPS", Price: 1000),
            new(Name: "iPhone 12 PRO Max", Price: 1500)
        };

        Accounts = new List<Account>
        {
            new(Number: 1000, Balance: 100_000),
            new(Number: 1001, Balance: 10_000)
        };
    }

    public IEnumerable<Product> Products { get; }
    public IEnumerable<Account> Accounts { get; }
}