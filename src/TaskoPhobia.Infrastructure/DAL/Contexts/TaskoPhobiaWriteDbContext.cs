using Microsoft.EntityFrameworkCore;
using TaskoPhobia.Core.Entities;
using TaskoPhobia.Core.Entities.Invitations;
using TaskoPhobia.Core.Entities.Projects;
using TaskoPhobia.Core.Entities.ProjectTasks;
using TaskoPhobia.Core.Entities.Users;
using TaskoPhobia.Infrastructure.DAL.Configurations.Write;

namespace TaskoPhobia.Infrastructure.DAL.Contexts;

internal sealed class TaskoPhobiaWriteDbContext : DbContext
{
    public TaskoPhobiaWriteDbContext(DbContextOptions<TaskoPhobiaWriteDbContext> options) : base(options)
    {
    }

    public DbSet<User> Users { get; set; }
    public DbSet<Project> Projects { get; set; }
    public DbSet<ProjectTask> ProjectTasks { get; set; }
    public DbSet<Invitation> Invitations { get; set; }
    public DbSet<ProjectParticipation> ProjectParticipations { get; set; }
    public DbSet<TaskAssignment> TaskAssignments { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.HasDefaultSchema("taskophobia");

        modelBuilder.ApplyConfiguration(new UserWriteConfiguration());
        modelBuilder.ApplyConfiguration(new ProjectWriteConfiguration());
        modelBuilder.ApplyConfiguration(new ProjectTaskWriteConfiguration());
        modelBuilder.ApplyConfiguration(new InvitationWriteConfiguration());
        modelBuilder.ApplyConfiguration(new ProjectParticipationWriteConfiguration());
        modelBuilder.ApplyConfiguration(new ProjectSummaryWriteConfiguration());
        modelBuilder.ApplyConfiguration(new TaskAssignmentWriteConfiguration());
    }
}