using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using TaskoPhobia.Application.Security;
using TaskoPhobia.Core.Entities.Users;
using TaskoPhobia.Core.ValueObjects;
using TaskoPhobia.Infrastructure.DAL.Contexts;
using TaskoPhobia.Shared.Abstractions.Time;

namespace TaskoPhobia.Infrastructure.DAL.DatabaseInitializer;

internal sealed class DatabaseInitializer : IHostedService
{
    private readonly DatabaseInitializerAdminOptions _adminOptions;
    private readonly IClock _clock;
    private readonly IPasswordManager _passwordManager;
    private readonly IServiceProvider _serviceProvider;

    public DatabaseInitializer(IOptions<DatabaseInitializerOptions> options, IServiceProvider serviceProvider,
        IClock clock, IPasswordManager passwordManager)
    {
        _serviceProvider = serviceProvider;
        _clock = clock;
        _passwordManager = passwordManager;
        _adminOptions = options.Value.Admin;
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        using (var scope = _serviceProvider.CreateScope())
        {
            var dbContext = scope.ServiceProvider.GetRequiredService<TaskoPhobiaWriteDbContext>();
            dbContext.Database.Migrate();

            var adminIsInDatabase = dbContext.Users.Any(user => user.Role.Equals(Role.Admin()));

            if (!adminIsInDatabase)
                dbContext.Users.Add(User.NewAdmin(Guid.NewGuid(), _adminOptions.Email,
                    _passwordManager.Secure(_adminOptions.Password),
                    _clock.Now()));

            dbContext.SaveChanges();
        }

        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}