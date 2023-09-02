using TaskoPhobia.Core.Entities.AccountUpgradeProducts;
using TaskoPhobia.Core.Entities.Users;
using TaskoPhobia.Shared.Abstractions.Domain;

namespace TaskoPhobia.Core.DomainServices.Orders.Rules;

internal sealed class AccountUpgradeCanNotBeLowerThanCurrentUserAccount : IBusinessRule
{
    private readonly AccountUpgradeProduct _accountUpgradeProduct;
    private readonly User _user;

    public AccountUpgradeCanNotBeLowerThanCurrentUserAccount(AccountUpgradeProduct accountUpgradeProduct, User user)
    {
        _accountUpgradeProduct = accountUpgradeProduct;
        _user = user;
    }

    public string Message =>
        $"You can't order upgrade to '{_accountUpgradeProduct.UpgradeTypeValue.Value}' if you have account of type '{_user.AccountType}'";

    public bool IsBroken()
    {
        var isExtendedAccountToBasicUpgrade = _user.HasExtendedAccount() &&
                                              _accountUpgradeProduct.UpgradesAccountToBasic();

        var isExtendedAccountToExtendedUpgrade =
            _user.HasExtendedAccount() && _accountUpgradeProduct.UpgradesAccountToExtended();

        var isBasicAccountToBasicUpgrade = _user.HasBasicAccount() && _accountUpgradeProduct.UpgradesAccountToBasic();

        return isExtendedAccountToBasicUpgrade ||
               isExtendedAccountToExtendedUpgrade ||
               isBasicAccountToBasicUpgrade;
    }
}