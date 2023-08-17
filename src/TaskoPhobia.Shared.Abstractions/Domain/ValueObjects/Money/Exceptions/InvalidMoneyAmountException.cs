using TaskoPhobia.Shared.Abstractions.Exceptions;

namespace TaskoPhobia.Shared.Abstractions.Domain.ValueObjects.Money.Exceptions;

public class InvalidMoneyAmountException : CustomException
{
    public InvalidMoneyAmountException(decimal amount) : base($"Money amount {amount}")
    {
    }
}