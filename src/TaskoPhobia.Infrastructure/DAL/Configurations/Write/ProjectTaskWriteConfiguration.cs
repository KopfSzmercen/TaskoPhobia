﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaskoPhobia.Core.Entities.ProjectTasks;
using TaskoPhobia.Core.ValueObjects;

namespace TaskoPhobia.Infrastructure.DAL.Configurations.Write;

internal sealed class ProjectTaskWriteConfiguration : IEntityTypeConfiguration<ProjectTask>
{
    public void Configure(EntityTypeBuilder<ProjectTask> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id)
            .HasConversion(x => x.Value,
                x => new ProjectTaskId(x));

        builder.Property(x => x.Name)
            .IsRequired()
            .HasConversion(x => x.Value,
                x => new ProjectTaskName(x));

        builder.OwnsOne(x => x.TimeSpan, timeSpan =>
        {
            timeSpan.Property(x => x.Start).IsRequired().HasColumnName("StartDate");
            timeSpan.Property(x => x.End).IsRequired().HasColumnName("EndDate");
        });

        builder.Property(x => x.Status)
            .IsRequired()
            .HasConversion(x => x.Value,
                x => new ProgressStatus(x));

        builder.HasOne(x => x.Project)
            .WithMany()
            .HasForeignKey(x => x.ProjectId)
            .IsRequired();

        builder.Property(x => x.ProjectId)
            .HasConversion(x => x.Value,
                x => new ProjectId(x));

        builder.Property(x => x.AssignmentsLimit)
            .HasConversion(x => x.Value, x => new TaskAssignmentsLimit(x))
            .HasDefaultValue(new TaskAssignmentsLimit(1))
            .IsRequired();

        builder.HasMany(x => x.Assignments)
            .WithOne()
            .HasForeignKey(x => x.TaskId)
            .IsRequired();
    }
}