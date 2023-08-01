using TaskoPhobia.Shared.Abstractions.Domain;

namespace TaskoPhobia.Shared.Abstractions.Exceptions;

public class BusinessRuleValidationException : Exception
{
    public BusinessRuleValidationException(IBusinessRule rule) : base(rule.Message)
    {
        BrokenRule = rule;
    }

    public IBusinessRule BrokenRule { get; }
}