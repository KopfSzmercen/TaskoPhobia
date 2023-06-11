using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaskoPhobia.Core.Entities;
using TaskoPhobia.Core.ValueObjects;

namespace TaskoPhobia.Infrastructure.DAL.Configurations;

internal sealed class ProjectTaskConfiguration : IEntityTypeConfiguration<ProjectTask>
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

        builder.HasOne<Project>(x => x.Project)
            .WithMany(p => p.Tasks)
            .HasForeignKey(x => x.ProjectId);

        builder.Property(x => x.ProjectId)
            .HasConversion(x => x.Value,
                x => new ProjectId(x));
    }
}