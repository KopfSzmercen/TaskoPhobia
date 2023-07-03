using Microsoft.EntityFrameworkCore;
using TaskoPhobia.Core.Entities;
using TaskoPhobia.Infrastructure.DAL.Configurations.Write;

namespace TaskoPhobia.Infrastructure.DAL.Contexts;

public class TaskoPhobiaWriteDbContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Project> Projects { get; set; }
    public DbSet<ProjectTask> ProjectTasks { get; set; }
    public TaskoPhobiaWriteDbContext(DbContextOptions<TaskoPhobiaWriteDbContext> options) : base(options)
    {
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.HasDefaultSchema("taskophobia");

        modelBuilder.ApplyConfiguration(new UserWriteConfiguration());
        modelBuilder.ApplyConfiguration(new ProjectWriteConfiguration());
        modelBuilder.ApplyConfiguration(new ProjectTaskWriteConfiguration());
    }
}