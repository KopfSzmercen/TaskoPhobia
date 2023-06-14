using TaskoPhobia.Application.DTO;
using TaskoPhobia.Core.Entities;

namespace TaskoPhobia.Infrastructure.DAL.Handlers;

public static class Extensions
{
    public static UserDto AsDto(this User entity)
    {
        return new UserDto
        {
            Id = entity.Id,
            Username = entity.Username,
            Email = entity.Email,
            Role = entity.Role
        };
    }

    public static ProjectDto AsDto(this Project entity)
    {
        return new ProjectDto
        {
            Id = entity.Id,
            Description = entity.Description,
            Name = entity.Name,
            Status = entity.Status,
            CreatedAt = entity.CreatedAt
        };
    }

    public static ProjectTaskDto AsDto(this ProjectTask entity)
    {
        return new ProjectTaskDto
        {
            Id = entity.Id,
            Name = entity.Name,
            Status = entity.Status,
            EndDate = entity.TimeSpan.End,
            StartDate = entity.TimeSpan.Start
        };
    }
}