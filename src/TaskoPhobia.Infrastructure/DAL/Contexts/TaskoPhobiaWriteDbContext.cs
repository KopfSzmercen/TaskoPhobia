﻿using Microsoft.EntityFrameworkCore;
using TaskoPhobia.Core.Entities;
using TaskoPhobia.Core.Entities.AccountUpgradeProducts;
using TaskoPhobia.Core.Entities.Invitations;
using TaskoPhobia.Core.Entities.Payments;
using TaskoPhobia.Core.Entities.Products;
using TaskoPhobia.Core.Entities.Projects;
using TaskoPhobia.Core.Entities.ProjectTasks;
using TaskoPhobia.Core.Entities.Users;
using TaskoPhobia.Infrastructure.DAL.Configurations.Write;
using TaskoPhobia.Shared.Abstractions.Outbox;

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
    public DbSet<OutboxMessage> OutboxMessages { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<AccountUpgradeProduct> AccountUpgradeProducts { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<Payment> Payments { get; set; }

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
        modelBuilder.ApplyConfiguration(new OutboxMessageWriteConfiguration());
        modelBuilder.ApplyConfiguration(new ProductWriteConfiguration());
        modelBuilder.ApplyConfiguration(new AccountUpgradeProductWriteConfiguration());
        modelBuilder.ApplyConfiguration(new OrderWriteConfiguration());
        modelBuilder.ApplyConfiguration(new PaymentWriteConfiguration());
    }
}