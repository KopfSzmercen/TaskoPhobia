using TaskoPhobia.Core.Entities.AccountUpgradeProducts;
using TaskoPhobia.Core.Entities.Users;
using TaskoPhobia.Core.ValueObjects;
using TaskoPhobia.Shared.Abstractions.Domain;

namespace TaskoPhobia.Core.DomainServices.Orders.Rules;

public class AccountUpgradeCanNotBeLowerThanCurrentUserAccount : IBusinessRule
{
    private readonly AccountUpgradeProduct _accountUpgradeProduct;
    private readonly User _user;

    public AccountUpgradeCanNotBeLowerThanCurrentUserAccount(AccountUpgradeProduct accountUpgradeProduct, User user)
    {
        _accountUpgradeProduct = accountUpgradeProduct;
        _user = user;
    }

    public string Message =>
        $"You can't order upgrade to {_accountUpgradeProduct.UpgradeTypeValue} if you have account of type {_user.AccountType}";

    public bool IsBroken()
    {
        return (_user.AccountType.Equals(AccountType.Extended()) &&
                _accountUpgradeProduct.UpgradeTypeValue.Value.Equals(AccountType.Basic()))
               ||
               (_user.AccountType.Equals(AccountType.Basic()) &&
                _accountUpgradeProduct.UpgradeTypeValue.Value.Equals(AccountType.Free()))
               ||
               (_user.AccountType.Equals(AccountType.Extended()) &&
                _accountUpgradeProduct.UpgradeTypeValue.Value.Equals(AccountType.Extended()))
            ;
    }
}