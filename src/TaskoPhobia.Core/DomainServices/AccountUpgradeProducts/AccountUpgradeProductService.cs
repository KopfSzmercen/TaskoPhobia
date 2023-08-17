using TaskoPhobia.Core.DomainServices.AccountUpgradeProducts.Rules;
using TaskoPhobia.Core.Entities.AccountUpgradeProducts;
using TaskoPhobia.Core.ValueObjects;
using TaskoPhobia.Shared.Abstractions.Domain;
using TaskoPhobia.Shared.Abstractions.Domain.ValueObjects.Money;

namespace TaskoPhobia.Core.DomainServices.AccountUpgradeProducts;

internal sealed class AccountUpgradeProductService : DomainService, IAccountUpgradeProductService
{
    public IEnumerable<AccountUpgradeProduct> CreateInitialProducts(
        IEnumerable<AccountUpgradeProduct> existingAccountUpgradeProducts)
    {
        CheckRule(new AccountUpgradeProductsAlreadyExistRule(existingAccountUpgradeProducts));

        var upgradeToBasicAccount = AccountUpgradeProduct.New(Guid.NewGuid(),
            $"Upgrade to {AccountType.Basic()}",
            Money.Create(59, "PLN"),
            "description", AccountType.Basic().Value);

        var upgradeToExtendedAccount = AccountUpgradeProduct.New(Guid.NewGuid(),
            $"Upgrade to {AccountType.Extended()}",
            Money.Create(99, "PLN"),
            "description", AccountType.Extended().Value);

        return new[]
        {
            upgradeToBasicAccount,
            upgradeToExtendedAccount
        };
    }
}