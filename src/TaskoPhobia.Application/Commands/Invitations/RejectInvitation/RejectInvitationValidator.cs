using FluentValidation;

namespace TaskoPhobia.Application.Commands.Invitations.RejectInvitation;

internal sealed class RejectInvitationValidator : AbstractValidator<RejectInvitationRequest>
{
    public RejectInvitationValidator()
    {
        RuleFor(x => x.BlockSendingMoreInvitations)
            .NotEmpty();
    }
}