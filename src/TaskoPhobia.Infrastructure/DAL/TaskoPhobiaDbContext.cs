using Microsoft.EntityFrameworkCore;
using TaskoPhobia.Core.Entities;

namespace TaskoPhobia.Infrastructure.DAL;

internal sealed class TaskoPhobiaDbContext : DbContext
{

    public DbSet<User> Users { get; set; }
    public DbSet<Project> Projects { get; set; }
    public TaskoPhobiaDbContext(DbContextOptions<TaskoPhobiaDbContext> dbContextOptions) : base(dbContextOptions)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
    }
}