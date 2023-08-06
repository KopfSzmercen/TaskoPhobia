using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaskoPhobia.Core.Entities.ProjectTasks;
using TaskoPhobia.Core.Entities.Users;
using TaskoPhobia.Core.ValueObjects;

namespace TaskoPhobia.Infrastructure.DAL.Configurations.Write;

internal sealed class TaskAssignmentWriteConfiguration : IEntityTypeConfiguration<TaskAssignment>
{
    public void Configure(EntityTypeBuilder<TaskAssignment> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasConversion(x => x.Value, x => new TaskAssignmentId(x));

        builder.Property(x => x.CreatedAt).IsRequired();

        builder.Property(x => x.TaskId)
            .HasConversion(x => x.Value, x => new ProjectTaskId(x));

        builder.Property(x => x.Id)
            .HasConversion(x => x.Value, x => new TaskAssignmentId(x));

        builder.HasOne<User>("Assignee")
            .WithMany()
            .HasForeignKey(x => x.AssigneeId)
            .IsRequired();
    }
}