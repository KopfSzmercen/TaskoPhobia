using System.Net.Http.Headers;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using TaskoPhobia.Application.DTO;
using TaskoPhobia.Application.Security;
using TaskoPhobia.Core.Entities.AccountUpgradeProducts;
using TaskoPhobia.Core.Entities.AccountUpgradeProducts.ValueObjects;
using TaskoPhobia.Core.Entities.Products;
using TaskoPhobia.Core.Entities.Projects;
using TaskoPhobia.Core.Entities.Users;
using TaskoPhobia.Core.ValueObjects;
using TaskoPhobia.Infrastructure.Auth;
using TaskoPhobia.Infrastructure.Security;
using TaskoPhobia.Shared.Abstractions.Domain.ValueObjects.Money;
using TaskoPhobia.Shared.Abstractions.Time;
using TaskoPhobia.Shared.Time;
using Xunit;

namespace TaskoPhobia.Tests.Integration.Controllers;

[Collection("api")]
public abstract class ControllerTests : IClassFixture<OptionsProvider>, IDisposable
{
    private readonly IAuthenticator _authenticator;
    private readonly IClock _clock;
    internal readonly TestDatabase _testDatabase;

    protected ControllerTests(OptionsProvider optionsProvider, IClock clock)
    {
        var app = new TaskoPhobiaTestApp(ConfigureServices);
        HttpClient = app.Client;
        var authOptions = optionsProvider.Get<AuthOptions>("auth");

        _authenticator = new Authenticator(new OptionsWrapper<AuthOptions>(authOptions), clock);

        _testDatabase = new TestDatabase();
        _clock = new Clock();
    }

    protected HttpClient HttpClient { get; }

    public void Dispose()
    {
        _testDatabase?.Dispose();
    }

    protected virtual void ConfigureServices(IServiceCollection services)
    {
    }

    protected JwtDto Authorize(Guid userId, string role)
    {
        var jwt = _authenticator.CreateToken(userId, role);
        HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwt.AccessToken);

        return jwt;
    }

    protected async Task<User> CreateUserAsync()
    {
        var passwordManager = new PasswordManager(new PasswordHasher<object>());
        var securedPassword = passwordManager.Secure("secret");

        var user = User.New(Guid.NewGuid(), "testUser2@t.pl",
            "testUser2", securedPassword, DateTime.UtcNow);

        await _testDatabase.WriteDbContext.Users.AddAsync(user);
        await _testDatabase.WriteDbContext.SaveChangesAsync();

        return user;
    }

    protected async Task<(Project, User)> CreateUserWithProjectAsync()
    {
        var passwordManager = new PasswordManager(new PasswordHasher<object>());
        var securedPassword = passwordManager.Secure("secret");

        var user = User.New(Guid.NewGuid(), "testUser@t.pl",
            "testUser", securedPassword, DateTime.UtcNow);

        await _testDatabase.WriteDbContext.Users.AddAsync(user);
        await _testDatabase.WriteDbContext.SaveChangesAsync();

        var project = Project.CreateNew(Guid.NewGuid(), "Project name", "Project description",
            _clock, user.Id);

        await _testDatabase.WriteDbContext.Projects.AddAsync(project);
        await _testDatabase.WriteDbContext.SaveChangesAsync();

        return (project, user);
    }

    protected async Task<(User, Order)> CreateOrderAsync()
    {
        var customer = User.New(Guid.NewGuid(), "email@e.pl", "username", "password", DateTime.Now);

        await _testDatabase.WriteDbContext.Users.AddAsync(customer);

        var product = AccountUpgradeProduct.New(Guid.NewGuid(),
            "name", Money.Create(12, "PLN"), "desc", new AccountUpgradeTypeValue(AccountType.Basic()));

        await _testDatabase.WriteDbContext.AccountUpgradeProducts.AddAsync(product);

        var order = Order.New(Guid.NewGuid(), product.Id, product.Price, customer.Id, DateTimeOffset.Now);

        await _testDatabase.WriteDbContext.Orders.AddAsync(order);

        await _testDatabase.WriteDbContext.SaveChangesAsync();

        return (customer, order);
    }
}