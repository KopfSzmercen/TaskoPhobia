using TaskoPhobia.Core.ValueObjects;

namespace TaskoPhobia.Core.Entities.AccountUpgradeProducts.ValueObjects;

public record AccountUpgradeTypeValue
{
    public AccountUpgradeTypeValue(string value)
    {
        if (value.Equals(AccountType.Free())) throw new InvalidAccountUpgradeTypeValueException();
        Value = value;
    }

    public string Value { get; }

    public static implicit operator AccountUpgradeTypeValue(string value)
    {
        return new AccountUpgradeTypeValue(value);
    }

    public static implicit operator string(AccountUpgradeTypeValue value)
    {
        return value.Value;
    }
}