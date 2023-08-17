using TaskoPhobia.Shared.Abstractions.Exceptions;

namespace TaskoPhobia.Core.Entities.AccountUpgradeProducts.ValueObjects;

public sealed class InvalidAccountUpgradeTypeValueException : CustomException
{
    public InvalidAccountUpgradeTypeValueException() : base("Invalid value of account upgrade product.")
    {
    }
}