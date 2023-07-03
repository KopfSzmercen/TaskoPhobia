using Microsoft.EntityFrameworkCore;
using TaskoPhobia.Core.Entities;

namespace TaskoPhobia.Infrastructure.DAL;

internal sealed class TaskoPhobiaDbContext : DbContext
{
    public TaskoPhobiaDbContext(DbContextOptions<TaskoPhobiaDbContext> dbContextOptions) : base(dbContextOptions)
    {
    }

    public DbSet<User> Users { get; set; }
    public DbSet<Project> Projects { get; set; }
    public DbSet<ProjectTask> ProjectTasks { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
    }
}