using System.Globalization;

namespace FourDBS.Saga.Database.Domain;

public record Money(decimal Value)
{
    public static implicit operator Money(decimal value)
    {
        return new Money(Value: value);
    }

    public override string ToString()
    {
        return Value.ToString(format: "C2", provider: CultureInfo.GetCultureInfo(name: "en-US"));
    }
}