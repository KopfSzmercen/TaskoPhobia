using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaskoPhobia.Infrastructure.DAL.Configurations.Read.Model;

namespace TaskoPhobia.Infrastructure.DAL.Configurations.Read;

internal sealed class ProjectParticipationReadConfiguration : IEntityTypeConfiguration<ProjectParticipationReadModel>
{
    public void Configure(EntityTypeBuilder<ProjectParticipationReadModel> builder)
    {
        builder.HasKey(x => new { x.ParticipantId, x.ProjectId });

        builder.ToTable("ProjectParticipations");
    }
}