using System.Net;
using System.Net.Http.Json;
using Microsoft.EntityFrameworkCore;
using Shouldly;
using TaskoPhobia.Application.DTO;
using TaskoPhobia.Core.Entities.AccountUpgradeProducts;
using TaskoPhobia.Core.Entities.AccountUpgradeProducts.ValueObjects;
using TaskoPhobia.Core.ValueObjects;
using TaskoPhobia.Shared.Abstractions.Domain.ValueObjects.Money;
using TaskoPhobia.Shared.Time;
using Xunit;

namespace TaskoPhobia.Tests.Integration.Controllers;

public class AccountUpgradeProductsControllerTests : ControllerTests
{
    [Fact]
    public async Task RequestedByAdmin_PostAccountUpgradeProducts_ShouldCreateAccountUpgradeProducts()
    {
        var admin = await _testDatabase.ReadDbContext.Users.Where(x => x.Role == Role.Admin().Value)
            .SingleOrDefaultAsync();

        Authorize(admin.Id, admin.Role);

        var response = await HttpClient.PostAsJsonAsync("/account-upgrade-products", new object());

        response.StatusCode.ShouldBe(HttpStatusCode.Created);

        var numberOfAccountUpgradeProducts = await _testDatabase.ReadDbContext.AccountUpgradeProducts.CountAsync();

        numberOfAccountUpgradeProducts.ShouldBeGreaterThan(0);
    }

    [Fact]
    public async Task GetAccountUpgradeProducts_ShouldReturnAccountUpgradeProducts()
    {
        await SeedAccountUpgradeProducts();

        var response = await HttpClient.GetAsync("account-upgrade-products");

        response.StatusCode.ShouldBe(HttpStatusCode.OK);

        var results = response.Content.ReadFromJsonAsync<IEnumerable<AccountUpgradeProductDto>>();

        results.Result.Count().ShouldBeGreaterThan(0);
    }

    [Fact]
    public async Task GivenValidProductId_PostAccountUpgradeProductsOrders_ShouldCreateOrder()
    {
        await SeedAccountUpgradeProducts();

        var user = await CreateUserAsync();
        Authorize(user.Id, user.Role);

        var productInDb = await _testDatabase.ReadDbContext.AccountUpgradeProducts.FirstOrDefaultAsync();

        var response =
            await HttpClient.PostAsJsonAsync($"/account-upgrade-products/{productInDb.Id}/orders", new object());

        response.StatusCode.ShouldBe(HttpStatusCode.Created);

        var numOfOrdersInDb = await _testDatabase.ReadDbContext.Orders.CountAsync();
        numOfOrdersInDb.ShouldBe(1);
    }

    [Fact]
    public async Task GivenNonExistingProductId_PostAccountUpgradeProductsOrders_ShouldReturn_400BadRequestStatusCode()
    {
        var user = await CreateUserAsync();
        Authorize(user.Id, user.Role);

        var response =
            await HttpClient.PostAsJsonAsync($"/account-upgrade-products/{Guid.NewGuid().ToString()}/orders",
                new object());

        response.StatusCode.ShouldBe(HttpStatusCode.BadRequest);
    }


    #region Setup

    public AccountUpgradeProductsControllerTests(OptionsProvider optionsProvider) : base(optionsProvider, new Clock())
    {
    }


    private async Task SeedAccountUpgradeProducts()
    {
        await _testDatabase.WriteDbContext.AccountUpgradeProducts.AddAsync(AccountUpgradeProduct.New(Guid.NewGuid(),
            "name", Money.Create(12, "PLN"), "desc", new AccountUpgradeTypeValue(AccountType.Basic())));

        await _testDatabase.WriteDbContext.SaveChangesAsync();
    }

    #endregion
}