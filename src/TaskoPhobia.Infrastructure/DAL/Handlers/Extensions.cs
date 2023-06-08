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
}