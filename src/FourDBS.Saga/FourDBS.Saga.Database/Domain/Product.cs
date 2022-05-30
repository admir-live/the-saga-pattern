namespace FourDBS.Saga.Database.Domain;

public record Product(string Name, Money Price)
{
    public override string ToString()
    {
        return $"{Name} at price: {Price}";
    }
}