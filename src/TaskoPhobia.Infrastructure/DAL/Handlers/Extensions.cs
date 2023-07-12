using TaskoPhobia.Application.DTO;
using TaskoPhobia.Infrastructure.DAL.Configurations.Read.Model;

namespace TaskoPhobia.Infrastructure.DAL.Handlers;

internal static class Extensions
{
    public static UserDetailsDto AsDto(this UserReadModel user)
    {
        return new UserDetailsDto
        {
            Id = user.Id,
            Username = user.Username,
            Email = user.Email,
            Role = user.Role
        };
    }

    public static ProjectDto AsDto(this ProjectReadModel project)
    {
        return new ProjectDto
        {
            Id = project.Id,
            Description = project.Description,
            Name = project.Name,
            Status = project.Status,
            CreatedAt = project.CreatedAt
        };
    }

    public static ProjectTaskDto AsDto(this ProjectTaskReadModel projectTask)
    {
        return new ProjectTaskDto
        {
            Id = projectTask.Id,
            Name = projectTask.Name,
            Status = projectTask.Status,
            EndDate = projectTask.EndDate,
            StartDate = projectTask.StartDate
        };
    }

    public static ReceivedInvitationDto AsReceivedInvitationDto(this InvitationReadModel invitation)
    {
        return new ReceivedInvitationDto
        {
            Id = invitation.Id,
            Status = invitation.Status,
            CreatedAt = invitation.CreatedAt,
            Title = invitation.Title,
            Sender = new UserDto
            {
                Id = invitation.Sender.Id,
                Username = invitation.Sender.Username
            },
            Project = new ProjectDto
            {
                Id = invitation.ProjectId,
                Description = invitation.Project.Description,
                Name = invitation.Project.Name,
                Status = invitation.Project.Status,
                CreatedAt = invitation.Project.CreatedAt
            }
        };
    }

    public static SentInvitationDto AsSentInvitationDto(this InvitationReadModel invitation)
    {
        return new SentInvitationDto
        {
            Id = invitation.Id,
            Status = invitation.Status,
            CreatedAt = invitation.CreatedAt,
            Title = invitation.Title,
            Receiver = new UserDto
            {
                Id = invitation.Receiver.Id,
                Username = invitation.Receiver.Username
            }
        };
    }
}