using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaskoPhobia.Infrastructure.DAL.Configurations.Read.Model;

namespace TaskoPhobia.Infrastructure.DAL.Configurations.Read;

internal sealed class ProjectTaskAssignmentReadConfiguration : IEntityTypeConfiguration<ProjectTaskAssignmentReadModel>
{
    public void Configure(EntityTypeBuilder<ProjectTaskAssignmentReadModel> builder)
    {
        builder.HasKey(x => x.Id);

        builder.HasOne(x => x.User)
            .WithMany()
            .HasForeignKey(x => x.AssigneeId);

        builder.HasOne(x => x.ProjectTask)
            .WithMany(x => x.Assignments)
            .HasForeignKey(x => x.TaskId);


        builder.ToTable("TaskAssignments");
    }
}