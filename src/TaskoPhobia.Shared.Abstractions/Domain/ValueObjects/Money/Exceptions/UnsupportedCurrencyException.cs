using TaskoPhobia.Shared.Abstractions.Exceptions;

namespace TaskoPhobia.Shared.Abstractions.Domain.ValueObjects.Money.Exceptions;

public class UnsupportedCurrencyException : CustomException
{
    public UnsupportedCurrencyException(string message) : base(message)
    {
    }
}