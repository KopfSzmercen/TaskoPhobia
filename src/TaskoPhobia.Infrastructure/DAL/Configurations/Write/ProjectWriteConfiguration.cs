using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaskoPhobia.Core.Entities.Projects;
using TaskoPhobia.Core.ValueObjects;

namespace TaskoPhobia.Infrastructure.DAL.Configurations.Write;

internal sealed class ProjectWriteConfiguration : IEntityTypeConfiguration<Project>
{
    public void Configure(EntityTypeBuilder<Project> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id)
            .HasConversion(x => x.Value,
                x => new ProjectId(x));

        builder.Property(x => x.Name)
            .IsRequired()
            .HasConversion(x => x.Value,
                x => new ProjectName(x));

        builder.Property(x => x.Description)
            .HasConversion(x => x.Value,
                x => new ProjectDescription(x));

        builder.Property(x => x.Status)
            .IsRequired()
            .HasConversion(x => x.Value,
                x => new ProgressStatus(x));

        builder.Property(x => x.CreatedAt).IsRequired();

        builder.HasOne(project => project.Owner)
            .WithMany()
            .HasForeignKey(project => project.OwnerId)
            .IsRequired();

        builder.Property(x => x.OwnerId)
            .HasConversion(x => x.Value,
                x => new UserId(x));
    }
}