using Shouldly;
using TaskoPhobia.Shared.Abstractions.Domain;
using TaskoPhobia.Shared.Abstractions.Exceptions;

namespace TaskoPhobia.Tests.Unit;

public abstract class TestBase
{
    protected static void AssertBrokenRule<TRule>(TestDelegate testDelegate) where TRule : class, IBusinessRule
    {
        var businessRuleValidationException = Should.Throw<BusinessRuleValidationException>(() => testDelegate());
        businessRuleValidationException.BrokenRule.ShouldBeOfType<TRule>();
    }

    protected delegate void TestDelegate();
}