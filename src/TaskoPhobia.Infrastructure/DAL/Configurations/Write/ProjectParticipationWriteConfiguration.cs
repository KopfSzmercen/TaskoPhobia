using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaskoPhobia.Core.Entities;
using TaskoPhobia.Core.ValueObjects;

namespace TaskoPhobia.Infrastructure.DAL.Configurations.Write;

internal sealed class ProjectParticipationWriteConfiguration : IEntityTypeConfiguration<ProjectParticipation>
{
    public void Configure(EntityTypeBuilder<ProjectParticipation> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasConversion(x => x.Value,
                x => new ProjectParticipationId(x));

        builder.Property(x => x.ProjectId)
            .HasConversion(x => x.Value, x => new ProjectId(x));

        builder.Property(x => x.ParticipantId)
            .HasConversion(x => x.Value, x => new UserId(x));

        builder.Property(x => x.JoinDate)
            .IsRequired();

        builder.HasOne(x => x.Project)
            .WithMany(p => p.Participations)
            .HasForeignKey(x => x.ProjectId);

        builder.HasOne(x => x.Participant)
            .WithMany(u => u.ProjectParticipations)
            .HasForeignKey(x => x.ParticipantId);
    }
}