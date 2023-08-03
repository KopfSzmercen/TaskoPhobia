using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaskoPhobia.Core.Entities.ProjectTasks;
using TaskoPhobia.Core.ValueObjects;

namespace TaskoPhobia.Infrastructure.DAL.Configurations.Write;

internal sealed class ProjectSummaryWriteConfiguration : IEntityTypeConfiguration<ProjectSummary>
{
    public void Configure(EntityTypeBuilder<ProjectSummary> builder)
    {
        builder.ToTable("Projects");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasConversion(x => x.Value, x => new ProjectId(x));

        builder.Property(x => x.OwnerId)
            .HasColumnName("OwnerId")
            .HasConversion(x => x.Value,
                x => new UserId(x));

        builder.Property(x => x.Status)
            .HasColumnName("Status")
            .IsRequired()
            .HasConversion(x => x.Value,
                x => new ProgressStatus(x));
    }
}