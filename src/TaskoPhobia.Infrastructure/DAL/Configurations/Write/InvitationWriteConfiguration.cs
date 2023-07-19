using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaskoPhobia.Core.Entities.Invitations;
using TaskoPhobia.Core.ValueObjects;

namespace TaskoPhobia.Infrastructure.DAL.Configurations.Write;

internal sealed class InvitationWriteConfiguration : IEntityTypeConfiguration<Invitation>
{
    public void Configure(EntityTypeBuilder<Invitation> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasConversion(x => x.Value,
                x => new InvitationId(x))
            .IsRequired();

        builder.Property(x => x.Title)
            .HasConversion(x => x.Value,
                x => new InvitationTitle(x))
            .IsRequired();

        builder.Property(x => x.SenderId)
            .HasConversion(x => x.Value,
                x => new UserId(x))
            .IsRequired();

        builder.Property(x => x.ReceiverId)
            .HasConversion(x => x.Value,
                x => new UserId(x))
            .IsRequired();

        builder.Property(x => x.Status)
            .HasConversion(x => x.Value,
                x => new InvitationStatus(x))
            .IsRequired();

        builder.Property(x => x.ProjectId)
            .HasConversion(x => x.Value,
                x => new ProjectId(x))
            .IsRequired();

        builder.Property(x => x.CreatedAt)
            .IsRequired();

        builder.HasOne(invitation => invitation.Project)
            .WithMany(project => project.Invitations)
            .HasForeignKey(invitation => invitation.ProjectId);

        builder.HasOne(invitation => invitation.Sender)
            .WithMany(sender => sender.SentInvitations)
            .HasForeignKey(invitation => invitation.SenderId);

        builder.HasOne(invitation => invitation.Receiver)
            .WithMany(receiver => receiver.ReceivedInvitations)
            .HasForeignKey(invitation => invitation.ReceiverId);

        builder.Property(x => x.BlockSendingMoreInvitations)
            .IsRequired()
            .HasDefaultValue(false);
    }
}