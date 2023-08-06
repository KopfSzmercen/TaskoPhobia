using Microsoft.EntityFrameworkCore;
using TaskoPhobia.Infrastructure.DAL.Configurations.Read;
using TaskoPhobia.Infrastructure.DAL.Configurations.Read.Model;

namespace TaskoPhobia.Infrastructure.DAL.Contexts;

internal sealed class TaskoPhobiaReadDbContext : DbContext
{
    public TaskoPhobiaReadDbContext(DbContextOptions<TaskoPhobiaReadDbContext> options) : base(options)
    {
    }

    public DbSet<UserReadModel> Users { get; set; }
    public DbSet<ProjectReadModel> Projects { get; set; }
    public DbSet<ProjectTaskReadModel> ProjectTasks { get; set; }
    public DbSet<InvitationReadModel> Invitations { get; set; }
    public DbSet<ProjectParticipationReadModel> ProjectParticipations { get; set; }
    public DbSet<ProjectTaskAssignmentReadModel> ProjectTaskAssignments { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("taskophobia");
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfiguration(new UserReadConfiguration());
        modelBuilder.ApplyConfiguration(new ProjectReadConfiguration());
        modelBuilder.ApplyConfiguration(new ProjectTaskReadConfiguration());
        modelBuilder.ApplyConfiguration(new InvitationReadConfiguration());
        modelBuilder.ApplyConfiguration(new ProjectParticipationReadConfiguration());
        modelBuilder.ApplyConfiguration(new ProjectTaskAssignmentReadConfiguration());
    }
}