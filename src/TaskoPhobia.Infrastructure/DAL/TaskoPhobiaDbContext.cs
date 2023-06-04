using Microsoft.EntityFrameworkCore;

namespace TaskoPhobia.Infrastructure.DAL;

internal sealed class TaskoPhobiaDbContext : DbContext
{

    public TaskoPhobiaDbContext(DbContextOptions<TaskoPhobiaDbContext> dbContextOptions) : base(dbContextOptions)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
    }
}