using TaskoPhobia.Shared.Abstractions.Domain;

namespace TaskoPhobia.Shared.Abstractions.Exceptions;

public class BusinessRuleValidationException : CustomException
{
    public BusinessRuleValidationException(IBusinessRule rule) : base(rule.Message)
    {
        BrokenRule = rule;
    }

    public IBusinessRule BrokenRule { get; }
}