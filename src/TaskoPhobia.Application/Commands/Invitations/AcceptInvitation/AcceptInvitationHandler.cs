using TaskoPhobia.Application.Exceptions;
using TaskoPhobia.Core.DomainServices;
using TaskoPhobia.Core.Repositories;
using TaskoPhobia.Shared.Abstractions.Commands;
using TaskoPhobia.Shared.Abstractions.Contexts;

namespace TaskoPhobia.Application.Commands.Invitations.AcceptInvitation;

internal sealed class AcceptInvitationHandler : ICommandHandler<AcceptInvitation>
{
    private readonly IContext _context;
    private readonly IInvitationRepository _invitationRepository;
    private readonly IInvitationService _invitationService;

    public AcceptInvitationHandler(IInvitationService invitationService, IInvitationRepository invitationRepository,
        IContext context)
    {
        _invitationService = invitationService;
        _invitationRepository = invitationRepository;
        _context = context;
    }

    public async Task HandleAsync(AcceptInvitation command)
    {
        var invitation = await _invitationRepository.FindByIdAsync(command.InvitationId);
        if (invitation is null) throw new InvitationNotFoundException(command.InvitationId);

        _invitationService.AcceptInvitationAndJoinProject(invitation, _context.Identity.Id);

        await _invitationRepository.UpdateAsync(invitation);
    }
}