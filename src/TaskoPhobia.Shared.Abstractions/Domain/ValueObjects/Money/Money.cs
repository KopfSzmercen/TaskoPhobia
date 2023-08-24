using TaskoPhobia.Shared.Abstractions.Domain.ValueObjects.Money.Exceptions;

namespace TaskoPhobia.Shared.Abstractions.Domain.ValueObjects.Money;

public record Money
{
    private static readonly HashSet<string> AllowedCurrencies = new() { "PLN", "EUR", "GBP" };

    public Money()
    {
    }

    private Money(int amount, string currency)
    {
        if (amount is < 0 or > 1000000)
            throw new InvalidMoneyAmountException(amount);

        if (string.IsNullOrWhiteSpace(currency) || currency.Length != 3)
            throw new InvalidCurrencyException(currency);

        currency = currency.ToUpperInvariant();
        if (!AllowedCurrencies.Contains(currency))
            throw new UnsupportedCurrencyException(currency);

        Amount = amount;
        Currency = currency;
    }

    public int Amount { get; private set; }
    public string Currency { get; private set; }

    public static Money Create(int amount, string currency)
    {
        return new Money(amount, currency);
    }
}