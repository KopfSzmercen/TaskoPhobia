using TaskoPhobia.Core.Entities.AccountUpgradeProducts;
using TaskoPhobia.Core.ValueObjects;
using TaskoPhobia.Shared.Abstractions.Domain;

namespace TaskoPhobia.Core.DomainServices.AccountUpgradeProducts.Rules;

internal sealed class AccountUpgradeProductsAlreadyExistRule : IBusinessRule
{
    private readonly IEnumerable<AccountUpgradeProduct> _accountUpgradeProducts;

    public AccountUpgradeProductsAlreadyExistRule(IEnumerable<AccountUpgradeProduct> accountUpgradeProducts)
    {
        _accountUpgradeProducts = accountUpgradeProducts;
    }

    public string Message => "Account upgrade products already exist";

    public bool IsBroken()
    {
        return _accountUpgradeProducts.Any(x => x.UpgradeTypeValue == AccountType.Basic()) &&
               _accountUpgradeProducts.Any(x => x.UpgradeTypeValue == AccountType.Extended());
    }
}