using Microsoft.EntityFrameworkCore;
using TaskoPhobia.Infrastructure.DAL.Configurations.Read;
using TaskoPhobia.Infrastructure.DAL.Configurations.Read.Model;

namespace TaskoPhobia.Infrastructure.DAL.Contexts;

internal sealed class TaskoPhobiaReadDbContext : DbContext
{
    public DbSet<UserReadModel> Users { get; set; }
    public DbSet<ProjectReadModel> Projects { get; set; }
    public DbSet<ProjectTaskReadModel> ProjectTasks { get; set; }
    public TaskoPhobiaReadDbContext(DbContextOptions<TaskoPhobiaReadDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("taskophobia");
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfiguration(new UserReadConfiguration());
        modelBuilder.ApplyConfiguration(new ProjectReadConfiguration());
        modelBuilder.ApplyConfiguration(new ProjectTaskReadConfiguration());
    }
}