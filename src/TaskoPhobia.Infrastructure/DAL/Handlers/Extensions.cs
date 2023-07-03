using TaskoPhobia.Application.DTO;
using TaskoPhobia.Core.Entities;
using TaskoPhobia.Infrastructure.DAL.Configurations.Read.Model;

namespace TaskoPhobia.Infrastructure.DAL.Handlers;

public static class Extensions
{
    public static UserDto AsDto(this UserReadModel user)
    {
        return new UserDto
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
}