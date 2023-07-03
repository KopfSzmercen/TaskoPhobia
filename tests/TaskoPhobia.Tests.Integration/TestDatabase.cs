using Microsoft.EntityFrameworkCore;
using TaskoPhobia.Infrastructure.DAL;
using TaskoPhobia.Infrastructure.DAL.Contexts;

namespace TaskoPhobia.Tests.Integration;

internal class TestDatabase : IDisposable
{
    public TestDatabase()
    {
        var options = new OptionsProvider().Get<PostgresOptions>("database");
        
        
        
        ReadDbContext = new TaskoPhobiaReadDbContext(new DbContextOptionsBuilder<TaskoPhobiaReadDbContext>()
            .UseNpgsql(options.ConnectionString)
            .Options);
        
        
        WriteDbContext = new TaskoPhobiaWriteDbContext(new DbContextOptionsBuilder<TaskoPhobiaWriteDbContext>()
            .UseNpgsql(options.ConnectionString)
            .Options);
        
    }

    public TaskoPhobiaReadDbContext ReadDbContext { get; }
    public TaskoPhobiaWriteDbContext WriteDbContext { get; }

    public void Dispose()
    {
        WriteDbContext.Database.EnsureDeleted();
        WriteDbContext?.Dispose();
    }
}