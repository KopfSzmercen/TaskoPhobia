using TaskoPhobia.Core.DomainServices.Orders;
using TaskoPhobia.Core.DomainServices.Orders.Rules;
using TaskoPhobia.Core.Entities.Users;
using TaskoPhobia.Core.ValueObjects;
using Xunit;

namespace TaskoPhobia.Tests.Unit.Services;

public class OrdersServiceTests : TestBase
{
    [Fact]
    public void
        Trying_CreateOrderForAccountUpgradeProduct_Throw_AccountUpgradeCanNotBeLowerThanCurrentUserAccount_Exception()
    {
        var service = new OrdersService();

        var user = User.New(Guid.NewGuid(), "email@t.pl", "tttt", "password", DateTime.Now);
        user.SetAccountType(AccountType.Basic());

        AssertBrokenRule<AccountUpgradeCanNotBeLowerThanCurrentUserAccount>(() =>
            service.CreateOrderForAccountUpgradeProduct(Guid.NewGuid(),
                CreateUpgradeToBasicAccountProduct(), user, DateTimeOffset.Now));

        user.SetAccountType(AccountType.Extended());

        AssertBrokenRule<AccountUpgradeCanNotBeLowerThanCurrentUserAccount>(() =>
            service.CreateOrderForAccountUpgradeProduct(Guid.NewGuid(),
                CreateUpgradeToBasicAccountProduct(), user, DateTimeOffset.Now));
    }
}