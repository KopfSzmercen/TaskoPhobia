using TaskoPhobia.Core.Entities.AccountUpgradeProducts;
using TaskoPhobia.Core.Entities.Users;
using TaskoPhobia.Core.ValueObjects;
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
        var isExtendedAccountToBasicUpgrade = _user.AccountType.Equals(AccountType.Extended()) &&
                                              _accountUpgradeProduct.UpgradeTypeValue.Value.Equals(AccountType.Basic());

        var isBasicAccountToFreeUpgrade = _user.AccountType.Equals(AccountType.Basic()) &&
                                          _accountUpgradeProduct.UpgradeTypeValue.Value.Equals(AccountType.Free());

        var isExtendedAccountToExtendedUpgrade = _user.AccountType.Equals(AccountType.Extended()) &&
                                                 _accountUpgradeProduct.UpgradeTypeValue.Value.Equals(
                                                     AccountType.Extended());

        var isBasicAccountToBasicUpgrade = _user.AccountType.Equals(AccountType.Basic()) &&
                                           _accountUpgradeProduct.UpgradeTypeValue.Value.Equals(AccountType.Basic());

        return isExtendedAccountToBasicUpgrade ||
               isBasicAccountToFreeUpgrade ||
               isExtendedAccountToExtendedUpgrade ||
               isBasicAccountToBasicUpgrade;
    }
}