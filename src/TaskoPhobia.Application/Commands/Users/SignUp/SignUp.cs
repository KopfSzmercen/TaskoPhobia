using System.ComponentModel.DataAnnotations;
using TaskoPhobia.Shared.Abstractions.Commands;

namespace TaskoPhobia.Application.Commands.Users.SignUp;

public record SignUp(Guid UserId, string Email, string Username, string Password) : ICommand;