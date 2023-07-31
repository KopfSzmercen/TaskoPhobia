using TaskoPhobia.Shared.Abstractions.Exceptions;

namespace TaskoPhobia.Shared.Abstractions.Domain;

public abstract class DomainService
{
    protected void CheckRule(IBusinessRule rule)
    {
        if (rule.IsBroken()) throw new BusinessRuleValidationException(rule);
    }
}