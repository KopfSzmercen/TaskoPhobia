using TaskoPhobia.Core.Exceptions;

namespace TaskoPhobia.Core.ValueObjects;

public record AccountType
{
    // #CR notka odnośnie valueObjectów, nie wszystkie będą w tym Core, niektóre takie jak Email, mogą być wykorzystane w różnych miejscach i dobrze byłoby je wydzielić
    public static IEnumerable<string> AvailableAccountTypes { get; } = new[] {"free", "basic", "extended"};
    
    public string Value { get; }

    public AccountType(string value)
    {
        if (string.IsNullOrWhiteSpace(value) || value.Length > 30)
        {
            throw new InvalidAccountTypeException(value);
        }

        if (!AvailableAccountTypes.Contains(value))
        {
            throw new InvalidAccountTypeException(value);
        }

        Value = value;
    }

    public static AccountType Free() => new AccountType("free");
    public static AccountType Basic() => new AccountType("basic");
    public static AccountType Extended() => new AccountType("extended");

    public static implicit operator AccountType(string value) => new(value);
    public static implicit operator string(AccountType value) => value.Value;
    public override string ToString() => Value;
}