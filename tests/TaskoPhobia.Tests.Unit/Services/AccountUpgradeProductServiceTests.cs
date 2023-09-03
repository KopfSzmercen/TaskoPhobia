using TaskoPhobia.Core.DomainServices.AccountUpgradeProducts;
using TaskoPhobia.Core.DomainServices.AccountUpgradeProducts.Rules;
using Xunit;

namespace TaskoPhobia.Tests.Unit.Services;

public class AccountUpgradeProductServiceTests : TestBase
{
    [Fact]
    public void Trying_To_Create_Existing_Products_Should_Throw_AccountUpgradeProductsAlreadyExistRule_Exception()
    {
        var service = new AccountUpgradeProductService();

        AssertBrokenRule<AccountUpgradeProductsAlreadyExistRule>(() => service.CreateInitialProducts(
            new[]
            {
                CreateUpgradeToExtendedAccountProduct(),
                CreateUpgradeToBasicAccountProduct()
            }));
    }
}