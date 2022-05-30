namespace FourDBS.Saga.Database.Domain;

public record Account(int Number, Money Balance)
{
    public override string ToString()
    {
        return $"Account number: '{Number}' with current balance: {Balance}.";
    }
}