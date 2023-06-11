using TaskoPhobia.Application.DTO;
using TaskoPhobia.Core.Entities;

namespace TaskoPhobia.Infrastructure.DAL.Handlers;

public static class Extensions
{
    public static UserDto AsDto(this User entity) => new()
    {
        Id = entity.Id,
        Username = entity.Username,
        Email = entity.Email,
        Role = entity.Role
    };

    public static ProjectDto AsDto(this Project entity) => new()
    {
        Id = entity.Id,
        Description = entity.Description,
        Name = entity.Name,
        Status = entity.Status,
        CreatedAt = entity.CreatedAt
    };
}