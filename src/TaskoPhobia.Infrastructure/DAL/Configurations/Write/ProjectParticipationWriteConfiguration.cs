using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaskoPhobia.Core.Entities;

namespace TaskoPhobia.Infrastructure.DAL.Configurations.Write;

internal sealed class ProjectParticipationWriteConfiguration : IEntityTypeConfiguration<ProjectParticipation>
{
    public void Configure(EntityTypeBuilder<ProjectParticipation> builder)
    {
        builder.HasKey(x => new { x.ParticipantId, x.ProjectId });

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