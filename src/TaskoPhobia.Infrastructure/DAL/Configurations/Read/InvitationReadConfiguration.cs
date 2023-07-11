using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaskoPhobia.Infrastructure.DAL.Configurations.Read.Model;

namespace TaskoPhobia.Infrastructure.DAL.Configurations.Read;

internal sealed class InvitationReadConfiguration : IEntityTypeConfiguration<InvitationReadModel>
{
    public void Configure(EntityTypeBuilder<InvitationReadModel> builder)
    {
        builder.HasKey(x => x.Id);
        builder.ToTable("Invitations");
    }
}