using Microsoft.EntityFrameworkCore;
using TaskoPhobia.Infrastructure.DAL;

namespace TaskoPhobia.Tests.Integration;

internal class TestDatabase : IDisposable
{
    public TaskoPhobiaDbContext DbContext { get; }

    public TestDatabase()
    {
        var options = new OptionsProvider().Get<PostgresOptions>("database");
        DbContext = new TaskoPhobiaDbContext(new DbContextOptionsBuilder<TaskoPhobiaDbContext>()
            .UseNpgsql(options.ConnectionString)
            .Options);
    }

    public void Dispose()
    {
        DbContext.Database.EnsureDeleted();
        DbContext?.Dispose();
    }
}