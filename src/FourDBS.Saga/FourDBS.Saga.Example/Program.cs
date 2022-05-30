using FourDBS.Saga.Database;

namespace FourDBS.Saga.Example;

internal class Program
{
    private static void Main(string[] args)
    {
        //  Print();
        var simulateApiRequest = new SimulateApiRequest();
    }

    private static void Print()
    {
        Console.WriteLine(value: $"Accounts that are available in the database: {Environment.NewLine}{string.Join(separator: string.Empty, values: Enumerable.Repeat(element: "-", count: 60))}");
        foreach (var account in Static.Database.Accounts)
        {
            Console.WriteLine(value: account);
        }

        Console.WriteLine();

        Console.WriteLine(value: $"Products that are available in the database: {Environment.NewLine}{string.Join(separator: string.Empty, values: Enumerable.Repeat(element: "-", count: 60))}");
        foreach (var product in Static.Database.Products)
        {
            Console.WriteLine(value: product);
        }
    }
}