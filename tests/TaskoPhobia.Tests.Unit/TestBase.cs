using Shouldly;
using TaskoPhobia.Core.Entities.AccountUpgradeProducts;
using TaskoPhobia.Core.Entities.AccountUpgradeProducts.ValueObjects;
using TaskoPhobia.Core.ValueObjects;
using TaskoPhobia.Shared.Abstractions.Domain;
using TaskoPhobia.Shared.Abstractions.Domain.ValueObjects.Money;
using TaskoPhobia.Shared.Abstractions.Exceptions;

namespace TaskoPhobia.Tests.Unit;

public abstract class TestBase
{
    protected static void AssertBrokenRule<TRule>(TestDelegate testDelegate) where TRule : class, IBusinessRule
    {
        var businessRuleValidationException = Should.Throw<BusinessRuleValidationException>(() => testDelegate());
        businessRuleValidationException.BrokenRule.ShouldBeOfType<TRule>();
    }

    protected static async Task AssertBrokenRuleAsync<TRule>(AsyncTestDelegate testDelegate)
        where TRule : class, IBusinessRule
    {
        var requestAction = async () => await testDelegate();

        var businessRuleValidationException = await requestAction.ShouldThrowAsync<BusinessRuleValidationException>();

        businessRuleValidationException.BrokenRule.ShouldBeOfType<TRule>();
    }

    protected static AccountUpgradeProduct CreateUpgradeToBasicAccountProduct()
    {
        return AccountUpgradeProduct.New(Guid.NewGuid(), "name", Money.Create(12, "PLN"), "description",
            new AccountUpgradeTypeValue(AccountType.Basic()));
    }

    protected static AccountUpgradeProduct CreateUpgradeToExtendedAccountProduct()
    {
        return AccountUpgradeProduct.New(Guid.NewGuid(), "name", Money.Create(12, "PLN"), "description",
            new AccountUpgradeTypeValue(AccountType.Extended()));
    }

    protected delegate void TestDelegate();

    protected delegate Task AsyncTestDelegate();
}