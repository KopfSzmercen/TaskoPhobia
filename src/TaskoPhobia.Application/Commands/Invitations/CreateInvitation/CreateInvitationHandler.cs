using TaskoPhobia.Application.Exceptions;
using TaskoPhobia.Core.DomainServices.Invitations;
using TaskoPhobia.Core.Repositories;
using TaskoPhobia.Core.Services;
using TaskoPhobia.Shared.Abstractions.Commands;
using TaskoPhobia.Shared.Abstractions.Contexts;

namespace TaskoPhobia.Application.Commands.Invitations.CreateInvitation;

internal sealed class CreateInvitationHandler : ICommandHandler<CreateInvitation>
{
    private readonly IContext _context;
    private readonly IInvitationRepository _invitationRepository;
    private readonly IInvitationService _invitationService;
    private readonly IProjectRepository _projectRepository;
    private readonly IUserReadService _userReadService;

    public CreateInvitationHandler(IUserReadService userReadService, IProjectRepository projectRepository,
        IContext context, IInvitationService invitationService, IInvitationRepository invitationRepository)
    {
        _userReadService = userReadService;
        _projectRepository = projectRepository;
        _context = context;
        _invitationService = invitationService;
        _invitationRepository = invitationRepository;
    }

    public async Task HandleAsync(CreateInvitation command)
    {
        var senderId = _context.Identity.Id;

        var senderExists = await _userReadService.ExistsByIdAsync(senderId);
        if (!senderExists) throw new UserNotFoundException(senderId);

        var receiverExists = await _userReadService.ExistsByIdAsync(command.ReceiverId);
        if (!receiverExists) throw new UserNotFoundException(command.ReceiverId);

        var project = await _projectRepository.FindByIdAsync(command.ProjectId);
        if (project is null) throw new ProjectNotFoundException();

        var invitation = await _invitationService.CreateInvitation(command.InvitationId, project,
            senderId, command.ReceiverId);

        await _invitationRepository.AddAsync(invitation);
    }
}