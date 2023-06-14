using System.ComponentModel.DataAnnotations;
using TaskoPhobia.Shared.Abstractions.Commands;

namespace TaskoPhobia.Application.Commands.Users.SignUp;

public record SignUp(Guid UserId, [Required] string Email, [Required] string Username,
    [Required] string Password) : ICommand;